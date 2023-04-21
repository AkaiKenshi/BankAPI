using AutoMapper;
using BankAPI.DTOs.Customer;
using BankAPI.Data.Model;
using BankAPI.ServiceErrors;
using ErrorOr;
using System.Diagnostics.CodeAnalysis;

namespace BankAPI.Services.Customers;

public class CustomerService : ICustomerService
{
    public static readonly List<Customer> customers = new(){
        new Customer{
            Id = "1721489985",
            Username = "JIATN",
            FirstName = "Jose",
            LastName = "Alvarez Torres Naranjo",
            Email = "jiatn@live.com",
            Password = "1q2w3e4r5t"
        },
        new Customer{
            Id = "1721489993",
            Username = "JSATN",
            FirstName = "Juan",
            LastName = "Alvarez Torres Naranjo",
            Email = "jsatn@live.com",
            Password = "1a2s3d4f5g"
        }
    };
    private readonly IMapper _mapper;
    public CustomerService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest)
    {
        var newCustomer = _mapper.Map<Customer>(createRequest);

        if (customers.Any(c => c.Id.Equals(newCustomer.Id))) { return Errors.Customer.IdAlreadyExists; }
        else if (customers.Any(c => c.Username.Equals(newCustomer.Username))) { return Errors.Customer.UsernameAlreadyExists; }
        else if (customers.Any(c => c.Email.Equals(createRequest.Email))) { return Errors.Customer.EmailAlreadyExists; }

        customers.Add(newCustomer);
        return _mapper.Map<GetCustomerResponseDTO>(newCustomer);
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerByUsernameAsync(string username)
    {
        var customer = customers.FirstOrDefault(c => c.Username == username);
        if (customer == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    public async Task<ErrorOr<bool>> GetIdAvailable(string id)
    {
        return !customers.Any(c => c.Id == id);
    }

    public async Task<ErrorOr<bool>> GetUsernameAvailable(string username)
    {
        return !customers.Any(c => c.Username == username);
    }

    public async Task<ErrorOr<bool>> GetEmailAvailable(string email)
    {
        return !customers.Any(c => c.Email == email);
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) { return Errors.Customer.NotFound; }
        customer.LastName = updateRequest.LastName;
        customer.Username = updateRequest.FirstName;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerUsernameAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.Username.Equals(updateRequest.Username))) { return Errors.Customer.UsernameAlreadyExists; }

        customer.Username = updateRequest.Username;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.Email.Equals(updateRequest.Email))) { return Errors.Customer.EmailAlreadyExists; }
        else if (!customer.Password.Equals(updateRequest.Password)) { return Errors.Customer.InvalidPassword; }

        customer.Email = updateRequest.Email;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerIdAsync(string id, UpdateCustomerIdRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.Id.Equals(updateRequest.Id))) { return Errors.Customer.IdAlreadyExists; }
        else if (!customer.Password.Equals(updateRequest.Password)) { return Errors.Customer.InvalidPassword; }

        customer.Id = updateRequest.Id;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (!customer.Password.Equals(updateRequest.OldPassword)) { return Errors.Customer.InvalidPassword; }

        customer.Password = updateRequest.NewPassword;
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id)
    {
        int index = customers.FindIndex(c => c.Id.Equals(id));

        if (index == -1) { return Errors.Customer.NotFound; }

        customers.RemoveAt(index);
        return Result.Deleted;
    }

    public static bool FindIfUserExists(string id) => customers.Any(c => c.Id == id);
    
}
