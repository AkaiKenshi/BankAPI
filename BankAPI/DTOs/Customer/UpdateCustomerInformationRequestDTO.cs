namespace BankAPI.DTOs.Customers;

public record UpdateCustomerInformationRequestDTO
(
    string FirstName,
    string LastName
);