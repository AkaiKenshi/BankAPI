namespace BankAPI.DTOs.Customers;

public record GetCustomerLoginRequestDTO
(
    string Username, 
    string Password
);
