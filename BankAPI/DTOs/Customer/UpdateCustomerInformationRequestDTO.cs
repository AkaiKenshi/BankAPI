namespace BankAPI.DTOs.Customer;

public record UpdateCustomerInformationRequestDTO
(
    string CustomerName,
    string CustomerLastName
);