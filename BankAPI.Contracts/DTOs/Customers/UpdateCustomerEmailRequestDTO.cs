namespace BankAPI.Contracts.DTOs.Customers;

public record UpdateCustomerEmailRequestDTO
(
    string Email,
    string Password
);