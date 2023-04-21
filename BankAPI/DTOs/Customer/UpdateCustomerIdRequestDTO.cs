namespace BankAPI.DTOs.Customer;

public record UpdateCustomerIdRequestDTO(
    string Id, 
    string Password);
