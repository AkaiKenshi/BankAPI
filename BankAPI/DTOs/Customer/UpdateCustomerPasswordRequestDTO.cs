namespace BankAPI.DTOs.Customer;

public record UpdateCustomerPasswordRequestDTO(
    string OldPassword,
    string NewPassword
    );

