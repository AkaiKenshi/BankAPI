namespace BankAPI.DTOs.Customers;

public record UpdateCustomerPasswordRequestDTO(
    string OldPassword,
    string NewPassword
    );

