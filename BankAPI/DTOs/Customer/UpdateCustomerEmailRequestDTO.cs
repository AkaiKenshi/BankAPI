namespace BankAPI.DTOs.Customer;

public record UpdateCustomerEmailRequestDTO(
    string Email,
    string Password);