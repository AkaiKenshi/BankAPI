using BankAPI.DTOs.Account;
using ErrorOr;

namespace BankAPI.Services.Accounts;

public interface IAccountService
{
    Task<ErrorOr<GetAccountResponse>> CreateChekingAccountAsync(CreateCheckingAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponse>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponse>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponse>> GetAccountAsync(string  accountId);
    Task<ErrorOr<List<GetAccountResponse>>> GetListOfAccountsFromOwner(string accountOwnerId);
    Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request);
    Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request);
    Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request);
    Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId);
}
