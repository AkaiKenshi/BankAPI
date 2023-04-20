namespace BankAPI.DTOs.Account;

public record CreateFixedTermInvestmentAccountRequestDTO(
    double AccountBalance,
    string AccountOwner,
    int AccountTerm); 
