namespace BankAPI.Contracts.DTOs.Customers;

public record UpdateCustomerInformationRequestDTO
(
    string FirstName,
    string LastName
);