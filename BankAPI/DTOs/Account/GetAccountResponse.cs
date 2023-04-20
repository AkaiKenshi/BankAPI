using BankAPI.Model;

namespace BankAPI.DTOs.Account;

public record GetAccountResponse(
    string AccountId,
    double AccountBalance,
    AccountType AccountTypeId,
    string AccountOwner,
    DateOnly AccountCreatedDate,
    int? AccountTerm);
