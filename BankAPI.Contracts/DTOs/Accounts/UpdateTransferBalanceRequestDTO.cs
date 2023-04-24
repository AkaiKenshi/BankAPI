namespace BankAPI.Contracts.DTOs.Accounts;

public record UpdateTransferBalanceRequestDTO
(
    string Id,
    string TargetAccountId,
    double TransferAmount
);