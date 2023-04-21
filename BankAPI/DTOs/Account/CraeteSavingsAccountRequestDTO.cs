namespace BankAPI.DTOs.Account;

public record CreateSavingsAccountRequestDTO(
    double Balance,
    string OwnerId);