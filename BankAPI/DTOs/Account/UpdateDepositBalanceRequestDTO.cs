namespace BankAPI.DTOs.Account;

public record UpdateDepositBalanceRequestDTO(
    string Id, 
    double DepositAmount); 
