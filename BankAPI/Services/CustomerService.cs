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
            CustomerID = "0",
            CustomerUsername = "JIATN",
            CustomerName = "Jose",
            CustomerLastName = "Alvarez Torres Naranjo", 
            CustomerEmail = "jiatn@live.com", 
            CustomerPassword = "1q2w3e4r5t"
        },
        new Customer{
            CustomerID = "1",
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
        newCustomer.CustomerID = (customers.Max(c => int.Parse(c.CustomerID)) + 1).ToString();
        customers.Add(newCustomer);
        return _mapper.Map<GetCustomerResponseDTO>(newCustomer);     }


    public async Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id)
    {
        var result = customers.FirstOrDefault(c => c.CustomerID == id);
        if (result == null) { return Errors.Customer.NotFound; }
        return _mapper.Map<GetCustomerResponseDTO>(result);
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerUsernamAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<Updated>> UpdateCustomerPassword(string id, UpdateCustomerPasswordRequestDTO updateRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id)
    {
        throw new NotImplementedException();
    }
}
