namespace BankAPI.DTOs.Account;

public record UpdateWithdrawBalanceRequestDTO(
    string AccountId, 
    double WithdawAmount); 
