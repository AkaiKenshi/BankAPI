namespace BankAPI.DTOs.Customer;

public record UpdateCustomerPasswordRequestDTO(
    string CustomerOldPassword,
    string CustomerNewPassword
    );

