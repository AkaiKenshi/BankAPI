using AutoMapper;
using BankAPI.DTOs.Customer;
using BankAPI.Model;
using BankAPI.ServiceErrors;
using ErrorOr;

namespace BankAPI.Services;

public class CustomerService : ICustomerService
{
    private List<Customer> customers = new List<Customer>(){
        new Customer{
            CustomerId = "1721489985",
            CustomerUsername = "JIATN",
            CustomerName = "Jose",
            CustomerLastName = "Alvarez Torres Naranjo",
            CustomerEmail = "jiatn@live.com",
            CustomerPassword = "1q2w3e4r5t"
        },
        new Customer{
            CustomerId = "1721489993",
            CustomerUsername = "JSATN",
            CustomerName = "Juan",
            CustomerLastName = "Alvarez Torres Naranjo",
            CustomerEmail = "jsatn@live.com",
            CustomerPassword = "1a2s3d4f5g"
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
        
        if (customers.Any(c => c.CustomerId.Equals(newCustomer.CustomerId))) { return Errors.Customer.IdAlreadyExists; }
        else if (customers.Any(c => c.CustomerUsername.Equals(newCustomer.CustomerUsername))) { return Errors.Customer.UsernameAlredyExists; }
        else if (customers.Any(c => c.CustomerEmail.Equals(createRequest.CustomerEmail))) { return Errors.Customer.EmailAlreadyExists;  }

        customers.Add(newCustomer);
        return _mapper.Map<GetCustomerResponseDTO>(newCustomer);
    }


    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == id);
        if (customer == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(customer);
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId.Equals(id));
        if (customer == null) { return Errors.Customer.NotFound; }
        customer.CustomerLastName = updateRequest.CustomerLastName;
        customer.CustomerUsername = updateRequest.CustomerName;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerUsernamAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.CustomerUsername.Equals(updateRequest.CustomerUsername))) { return Errors.Customer.UsernameAlredyExists; }

        customer.CustomerUsername = updateRequest.CustomerUsername;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.CustomerEmail.Equals(updateRequest.CustomerEmail))) { return Errors.Customer.EmailAlreadyExists; }
        else if (customer.CustomerPassword.Equals(updateRequest.CustomerPassword)) { return Errors.Customer.InvalidPassword; }

        customer.CustomerEmail = updateRequest.CustomerEmail;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerIdAsync(string id, UpdateCustomerIdRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customers.Any(c => c.CustomerId.Equals(updateRequest.CustomerId))) { return Errors.Customer.IdAlreadyExists; }
        else if (customer.CustomerPassword.Equals(updateRequest.CustomerPassword)) { return Errors.Customer.InvalidPassword; }

        customer.CustomerId = updateRequest.CustomerId;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (customer == null) { return Errors.Customer.NotFound; }
        else if (customer.CustomerPassword.Equals(updateRequest.CustomerOldPassword)) { return  Errors.Customer.InvalidPassword; }
        
        customer.CustomerPassword = updateRequest.CustomerNewPassword;
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id)
    {
        int index =  customers.FindIndex(c => c.CustomerId.Equals(id));
        
        if (index == -1) {  return Errors.Customer.NotFound; }

        customers.RemoveAt(index);
        return Result.Deleted;
    }
}
