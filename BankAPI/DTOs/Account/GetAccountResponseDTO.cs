using BankAPI.Model;

namespace BankAPI.DTOs.Account;

public record GetAccountResponseDTO(
    string AccountId,
    double AccountBalance,
    AccountType AccountTypeId,
    string AccountOwnerId,
    DateOnly AccountCraetedDate,
    int? AccountTerm);
