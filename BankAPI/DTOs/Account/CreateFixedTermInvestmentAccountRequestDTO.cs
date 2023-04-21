namespace BankAPI.DTOs.Account;

public record CreateFixedTermInvestmentAccountRequestDTO(
    double Balance,
    string OwnerId,
    int Term); 
