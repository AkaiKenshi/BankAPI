namespace BankAPI.DTOs.Customer;

public record GetCustomerResponseDTO
(
    string Id,
    string Username,
    string FirstName,
    string LastName,
    string Email
);
