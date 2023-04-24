namespace BankAPI.DTOs.Account;

public record CreateFixedTermInvestmentAccountRequestDTO(
    double Balance,
    int Term); 
