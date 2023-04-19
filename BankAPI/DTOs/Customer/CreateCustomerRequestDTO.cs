namespace BankAPI.DTOs.Customer;

public record CreateCustomerRequestDTO
(
    string CustomerUsername,
    string CustomerName,
    string CustomerLastName,
    string CustomerEmail,
    string CustomerPassword
);