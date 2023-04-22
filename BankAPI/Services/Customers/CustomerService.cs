using AutoMapper;
using BankAPI.DTOs.Customer;
using BankAPI.Data.Model;
using BankAPI.ServiceErrors;
using ErrorOr;
using System.Diagnostics.CodeAnalysis;
using BankAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly BankDataContext _context;

    public CustomerService(IMapper mapper, BankDataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest)
    {
        var newCustomer = _mapper.Map<Customer>(createRequest);

        if (await _context.Customers.AnyAsync(c => c.Id.Equals(newCustomer.Id))) { return Errors.Customer.IdAlreadyExists; }
        else if (await _context.Customers.AnyAsync(c => c.Username.Equals(newCustomer.Username))) { return Errors.Customer.UsernameAlreadyExists; }
        else if (await _context.Customers.AnyAsync(c => c.Email.Equals(createRequest.Email))) { return Errors.Customer.EmailAlreadyExists; }

        await _context.AddAsync(newCustomer);

        _context.SaveChanges();
        return _mapper.Map<GetCustomerResponseDTO>(newCustomer);
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id); 
        if (customer == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerByUsernameAsync(string username)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        if (customer == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    public async Task<ErrorOr<bool>> GetIdAvailable(string id)
    {
        return !(await _context.Customers.AnyAsync(c => c.Id == id));
    }

    public async Task<ErrorOr<bool>> GetUsernameAvailable(string username)
    {
        return !(await _context.Customers.AnyAsync(c => c.Username == username));
    }

    public async Task<ErrorOr<bool>> GetEmailAvailable(string email)
    {
        return !(await _context.Customers.AnyAsync(c => c.Email == email));
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        
        if (customer == null) { return Errors.Customer.NotFound; }
        customer.LastName = updateRequest.LastName;
        customer.Username = updateRequest.FirstName;

        _context.SaveChanges();

        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerUsernameAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (await _context.Customers.AnyAsync(c => c.Username.Equals(updateRequest.Username))) { return Errors.Customer.UsernameAlreadyExists; }

        customer.Username = updateRequest.Username;

        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (await _context.Customers.AnyAsync(c => c.Email.Equals(updateRequest.Email))) { return Errors.Customer.EmailAlreadyExists; }
        else if (!customer.Password.Equals(updateRequest.Password)) { return Errors.Customer.InvalidPassword; }

        customer.Email = updateRequest.Email;

        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerIdAsync(string id, UpdateCustomerIdRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (await _context.Customers.AnyAsync(c => c.Id.Equals(updateRequest.Id))) { return Errors.Customer.IdAlreadyExists; }
        else if (!customer.Password.Equals(updateRequest.Password)) { return Errors.Customer.InvalidPassword; }

        customer.Id = updateRequest.Id;
        
        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (!customer.Password.Equals(updateRequest.OldPassword)) { return Errors.Customer.InvalidPassword; }

        customer.Password = updateRequest.NewPassword;
        
        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id)
    {
        var customer =  await _context.Customers.FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (customer == null) { return Errors.Customer.NotFound; }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return Result.Deleted;
    }

}
