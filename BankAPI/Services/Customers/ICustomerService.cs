using BankAPI.Contracts.DTOs.Customers;
using ErrorOr;

namespace BankAPI.Services.Customers;

public interface ICustomerService
{
    Task<ErrorOr<GetCustomerResponseDTO>> CreateCustomerAsync(CreateCustomerRequestDTO createRequest);
    Task<bool> GetIdAvailable(string id);
    Task<bool> GetUsernameAvailable(string username);
    Task<bool> GetEmailAvailable(string email);
    bool GetVaildPassword(string password);
    Task<ErrorOr<GetCustomerResponseDTO>> GetLoginCustomer(GetCustomerLoginRequestDTO loginRequest);
    Task<ErrorOr<Updated>> UpdateCustomerInformationAsync(string id, UpdateCustomerInformationRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerUsernameAsync(string id, UpdateCustomerUsernameRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerEmailAsync(string id, UpdateCustomerEmailRequestDTO updateRequest);
    Task<ErrorOr<Updated>> UpdateCustomerPasswordAsync(string id, UpdateCustomerPasswordRequestDTO updateRequest);
    Task<ErrorOr<Deleted>> DeleteCustomerAsync(string id);
}
