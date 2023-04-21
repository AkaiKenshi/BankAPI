using BankAPI.DTOs.Account;
using ErrorOr;

namespace BankAPI.Services.Accounts;

public interface IAccountService
{
    Task<ErrorOr<GetAccountResponseDTO>> CreateChekingAccountAsync(CreateCheckingAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponseDTO>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponseDTO>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request);
    Task<ErrorOr<GetAccountResponseDTO>> GetAccountAsync(string  accountId);
    Task<ErrorOr<List<GetAccountResponseDTO>>> GetListOfAccountsFromOwnerAsync(string accountOwnerId);
    Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request);
    Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request);
    Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request);
    Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId);
}
