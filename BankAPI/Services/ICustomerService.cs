using BankAPI.DTOs.Customer;
using BankAPI.Model;
using ErrorOr;

namespace BankAPI.Services;

public interface ICustomerService
{
    Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest);
    Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id);
    Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest); 
    Task<ErrorOr<Updated>> UpdateCustomerUsernamAsync(string id,  UpdateCustomerUsernameRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerPassword(string id, UpdateCustomerPasswordRequestDTO updateRequest);
    Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id);
}
