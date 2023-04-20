namespace BankAPI.DTOs.Customer;

public record UpdateCustomerInformationRequestDTO
(
    string CustomerFirstName,
    string CustomerLastName
);