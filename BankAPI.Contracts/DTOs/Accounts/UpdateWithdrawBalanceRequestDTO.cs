namespace BankAPI.Contracts.DTOs.Accounts;

public record UpdateWithdrawBalanceRequestDTO
(
    string Id,
    double WithdawAmount
);