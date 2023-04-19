namespace BankAPI.DTOs.Customer;

public record GetCustomerResponseDTO
(
    string CustomerID,
    string CustomerUsername,
    string CustomerName,
    string CustomerLastName,
    string CustomerEmail
);
