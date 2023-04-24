namespace BankAPI.Contracts.DTOs.Accounts;

public record UpdateDepositBalanceRequestDTO
(
    string Id,
    double DepositAmount
);