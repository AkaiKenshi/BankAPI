namespace BankAPI.DTOs.Customer;

public record UpdateCustomerInformationRequestDTO
(
    string CustomerUsername,
    string CustomerName,
    string CustomerLastName
);