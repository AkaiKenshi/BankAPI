using static BankAPI.ServiceErrors.Errors;
using BankAPI.DTOs.Customers;
using BankAPI.ServiceErrors;
using BankAPI.Data;
using ErrorOr;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace BankAPI.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly BankDataContext _context;
    private readonly IConfiguration _configuration;

    public CustomerService(IMapper mapper, BankDataContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _context = context;
        _configuration = configuration;
    }


    //Create 
    public async Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest)
    {
        var newCustomer = _mapper.Map<Data.Model.Customer>(createRequest);

        if (string.IsNullOrWhiteSpace(createRequest.FirstName) || string.IsNullOrWhiteSpace(createRequest.LastName) 
            || string.IsNullOrWhiteSpace(createRequest.Username)) { return Errors.Customer.IlligalData; }
        else if (!ValidatePassword(createRequest.Password)) { return Errors.Customer.IlligalPassword; }
        else if (!ValidateId(createRequest.Id)) { return Errors.Customer.IlligalId; }
        else if (!ValidateEmail(createRequest.Email)) { return Errors.Customer.IlligalEmail;  }
        else if (!await GetIdAvailable(createRequest.Id)) { return Errors.Customer.IdAlreadyExists; }
        else if (!await GetUsernameAvailable(createRequest.Username)) { return Errors.Customer.UsernameAlreadyExists; }
        else if (!await GetEmailAvailable(createRequest.Email)) { return Errors.Customer.EmailAlreadyExists; }

        CreatePasswordHash(createRequest.Password, out var passwordHash, out var passwordSalt);

        newCustomer.PasswordSalt = passwordSalt;
        newCustomer.PasswordHash = passwordHash;
        newCustomer.Token = CreateToken(newCustomer);

        await _context.AddAsync(newCustomer);

        await _context.SaveChangesAsync();
        return _mapper.Map<GetCustomerResponseDTO>(newCustomer);
    }


    //Gets
    public async Task<bool> GetIdAvailable(string id) =>
         !(ValidateId(id) || await _context.Customers.AnyAsync(c => c.Id == id));

    public async Task<bool> GetUsernameAvailable(string username) =>
        !(!string.IsNullOrWhiteSpace(username) || await _context.Customers.AnyAsync(c => c.Username == username));

    public async Task<bool> GetEmailAvailable(string email) =>
         !(ValidateEmail(email) || await _context.Customers.AnyAsync(c => c.Email.ToLower() == email.ToLower()));

    public bool GetVaildPassword(string password) =>
        ValidatePassword(password); 

    public async Task<ErrorOr<GetCustomerResponseDTO>> GetLoginCustomer(GetCustomerLoginRequestDTO loginRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == loginRequest.Username);

        if (customer == null
            || VerifyPasswordHash(loginRequest.Password, customer.PasswordHash, customer.PasswordSalt))
        {
            return Errors.Customer.InvalidLogin;
        }

        customer.Token = CreateToken(customer);

        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    //Updates
    public async Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        customer.LastName = updateRequest.LastName;
        customer.FirstName = updateRequest.FirstName;

        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerUsernameAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (await _context.Customers.AnyAsync(c => c.Username.Equals(updateRequest.Username))) { return Errors.Customer.UsernameAlreadyExists; }

        customer.Username = updateRequest.Username;

        await _context.SaveChangesAsync();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (await _context.Customers.AnyAsync(c => c.Email.Equals(updateRequest.Email))) { return Errors.Customer.EmailAlreadyExists; }

        customer.Email = updateRequest.Email;

        await _context.SaveChangesAsync();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (VerifyPasswordHash(updateRequest.OldPassword, customer.PasswordHash, customer.PasswordSalt)) { return Errors.Customer.InvalidPassword; }
        else if (ValidatePassword(updateRequest.NewPassword)) { return Errors.Customer.IlligalPassword; }

        CreatePasswordHash(updateRequest.NewPassword, out var passwordHash, out var passwordSalt);

        customer.PasswordHash = passwordHash;
        customer.PasswordSalt = passwordSalt;

        await _context.SaveChangesAsync();
        return Result.Updated;
    }


    //Delete
    public async Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return Result.Deleted;
    }


    #region helper functions

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return passwordHash.Equals(computedHash);
    }

    private string CreateToken(Data.Model.Customer customer)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, customer.Id),
            new Claim(ClaimTypes.Name, customer.Username)
        };

        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
        if (appSettingsToken == null) { throw new ArgumentNullException(nameof(appSettingsToken)); }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private bool ValidatePassword(string password)
    {
        string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";
        if (string.IsNullOrWhiteSpace(password)) { return false; }
        return Regex.IsMatch(password, pattern);
    }

    private bool ValidateId(string? id)
    {
        var sum = 0;
        if (string.IsNullOrWhiteSpace(id)
            || id.Length != 10
            || !int.TryParse(id, out _)
            || int.Parse(id[..2]) > 24
            || int.Parse(id[3].ToString()) > 6) { return false; }

        for (int i = 0; i < 8; i++)
        {
            var num = int.Parse(id[i].ToString());
            num = (num > 9) ? num - 9 : num;
            sum += num * ((i + 1) % 2 + 1);
        }
        var checkNum = ((sum / 10) + 1) * 10;

        return (checkNum - sum) == int.Parse(id[9].ToString());
    }

    private bool ValidateEmail(string email)
    {
        string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"; 
        if (string.IsNullOrWhiteSpace(email)) { return false;}
        return Regex.IsMatch(email, pattern);
    }

    #endregion

}
