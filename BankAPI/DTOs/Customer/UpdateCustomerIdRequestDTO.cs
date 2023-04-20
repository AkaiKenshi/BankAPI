namespace BankAPI.DTOs.Customer;

public record UpdateCustomerIdRequestDTO(
    string CustomerId, 
    string CustomerPassword);
