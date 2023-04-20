namespace BankAPI.DTOs.Account;

public record CreateSavingsAccountRequestDTO(
    double AccountBalance,
    string AccountOwner);