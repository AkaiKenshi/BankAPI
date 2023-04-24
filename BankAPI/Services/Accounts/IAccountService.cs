using BankAPI.Contracts.DTOs.Accounts;
using ErrorOr;

namespace BankAPI.Services.Accounts;

public interface IAccountService
{
    Task<ErrorOr<GetAccountResponseDTO>> CreateChekingAccountAsync(CreateCheckingAccountRequestDTO request, string customerId);
    Task<ErrorOr<GetAccountResponseDTO>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request, string customerId);
    Task<ErrorOr<GetAccountResponseDTO>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request, string customerId);
    Task<ErrorOr<GetAccountResponseDTO>> GetAccountAsync(string  accountId, string customerId);
    Task<ErrorOr<List<GetAccountResponseDTO>>> GetListOfAccountsFromOwnerAsync(string accountOwnerId);
    Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request, string customerId);
    Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request, string customerId);
    Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request, string customerId);
    Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId, string customerId);
}
