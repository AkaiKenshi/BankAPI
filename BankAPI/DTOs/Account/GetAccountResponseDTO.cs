using BankAPI.Data.Model;

namespace BankAPI.DTOs.Account;

public record GetAccountResponseDTO(
    string Id,
    double Balance,
    AccountType AccountType,
    string OwnerId,
    DateOnly CraetedDate,
    int? Term);
