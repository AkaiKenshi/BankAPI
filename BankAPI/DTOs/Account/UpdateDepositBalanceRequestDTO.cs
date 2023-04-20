namespace BankAPI.DTOs.Account;

public record UpdateDepositBalanceRequestDTO(
    string AccountId, 
    double DepositAmount); 
