namespace BankAPI.DTOs.Customers;

public record UpdateCustomerEmailRequestDTO
(
    string Email,
    string Password
);