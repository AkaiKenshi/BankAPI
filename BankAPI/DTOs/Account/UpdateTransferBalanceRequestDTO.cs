namespace BankAPI.DTOs.Account;

public record UpdateTransferBalanceRequestDTO(
    string AccountId,
    string TargetAccountId,
    double TransferAmount); 
