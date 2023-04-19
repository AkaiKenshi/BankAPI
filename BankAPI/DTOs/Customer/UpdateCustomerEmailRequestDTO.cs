namespace BankAPI.DTOs.Customer;

public record UpdateCustomerEmailRequestDTO(
    string CustomerEmail,
    string password);