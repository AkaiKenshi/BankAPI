namespace BankAPI.DTOs.Customer;

public record GetCustomerResponseDTO
(
    string CustomerID,
    string CustomerUsername,
    string CustomerFirstName,
    string CustomerLastName,
    string CustomerEmail
);
