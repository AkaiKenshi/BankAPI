namespace BankAPI.Contracts.DTOs.Customers;

public record UpdateCustomerPasswordRequestDTO(
    string OldPassword,
    string NewPassword
    );

