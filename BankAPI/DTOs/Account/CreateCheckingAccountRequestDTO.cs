namespace BankAPI.DTOs.Account;

public record CreateCheckingAccountRequestDTO(
    double AccountBalance, 
    string AccountOwner);
