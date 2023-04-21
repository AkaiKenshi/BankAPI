namespace BankAPI.DTOs.Customer;

public record CreateCustomerRequestDTO
(
    string Id,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password
);