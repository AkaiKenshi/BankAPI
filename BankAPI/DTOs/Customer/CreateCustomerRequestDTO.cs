namespace BankAPI.DTOs.Customer;

public record CreateCustomerRequestDTO
(
    string CustomerId,
    string CustomerUsername,
    string CustomerFirtsName,
    string CustomerLastName,
    string CustomerEmail,
    string CustomerPassword
);