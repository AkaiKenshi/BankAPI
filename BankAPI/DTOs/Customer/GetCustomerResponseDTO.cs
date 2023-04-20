namespace BankAPI.DTOs.Customer;

public record GetCustomerResponseDTO
(
    string CustomerId,
    string CustomerUsername,
    string CustomerFirstName,
    string CustomerLastName,
    string CustomerEmail
);
