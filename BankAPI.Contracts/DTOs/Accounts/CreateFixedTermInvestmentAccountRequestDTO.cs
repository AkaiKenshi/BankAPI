namespace BankAPI.Contracts.DTOs.Accounts;

public record CreateFixedTermInvestmentAccountRequestDTO
(
    double Balance,
    int Term
);
