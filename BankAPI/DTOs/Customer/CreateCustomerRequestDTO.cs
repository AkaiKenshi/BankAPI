namespace BankAPI.DTOs.Customer;

public record CreateCustomerRequestDTO
(
    string customerId,
    string CustomerUsername,
    string CustomerFirtsName,
    string CustomerLastName,
    string CustomerEmail,
    string CustomerPassword
);