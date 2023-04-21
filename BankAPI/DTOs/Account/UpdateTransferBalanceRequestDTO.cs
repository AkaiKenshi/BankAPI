namespace BankAPI.DTOs.Account;

public record UpdateTransferBalanceRequestDTO(
    string Id,
    string TargetAccountId,
    double TransferAmount); 
