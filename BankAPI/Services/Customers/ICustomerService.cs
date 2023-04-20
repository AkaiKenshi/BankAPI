using BankAPI.DTOs.Customer;
using BankAPI.Model;
using ErrorOr;

namespace BankAPI.Services.Customers;

public interface ICustomerService
{
    Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest);
    Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerAsync(string id);
    Task<ErrorOr<GetCustomerResponseDTO>> GetCustomerByUsernameAsync(string username);
    Task<ErrorOr<bool>> GetIdAvailable(string id);
    Task<ErrorOr<bool>> GetUsernameAvailable(string username);
    Task<ErrorOr<bool>> GetEmailAvailable(string email);
    Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerUsernameAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerIdAsync(string id, UpdateCustomerIdRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest);
    Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id);
}
