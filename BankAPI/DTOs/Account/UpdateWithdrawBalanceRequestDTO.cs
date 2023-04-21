namespace BankAPI.DTOs.Account;

public record UpdateWithdrawBalanceRequestDTO(
    string Id, 
    double WithdawAmount); 
