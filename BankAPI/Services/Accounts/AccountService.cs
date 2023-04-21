using AutoMapper;
using BankAPI.DTOs.Account;
using BankAPI.Model;
using BankAPI.ServiceErrors;
using BankAPI.Services.Customers;
using ErrorOr;

namespace BankAPI.Services.Accounts;

public class AccountService : IAccountService
{
    private static readonly List<Account> accounts = new()
    {
        new Account
        {
            AccountId = "0000000001",
            AccountBalance = 100,
            AccountTypeId = AccountType.Checking,
            AccountOwnerId = "1721489985",
            AccountCraetedDate = new DateOnly(2023,4,20)
        },
        new Account
        {
            AccountId = "0000000002",
            AccountBalance = 300,
            AccountTypeId = AccountType.Checking,
            AccountOwnerId = "1721489993",
            AccountCraetedDate = new DateOnly(2023,4,20)
        }
    };
    private readonly IMapper _mapper;

    public AccountService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<GetAccountResponseDTO>> CreateChekingAccountAsync(CreateCheckingAccountRequestDTO request)
    {
        var newAccount = _mapper.Map<Account>(request);
        if(CustomerService.FindIfUserExists(request.AccountOwner)) { return Errors.Customer.NotFound; }
        newAccount.AccountId = (accounts.Max(a => int.Parse(a.AccountId)) + 1).ToString("D10");
        newAccount.AccountTypeId = AccountType.Checking;
        newAccount.AccountCraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request)
    {
        var newAccount = _mapper.Map<Account>(request);
        if (CustomerService.FindIfUserExists(request.AccountOwner)) { return Errors.Customer.NotFound; }
        newAccount.AccountId = (accounts.Max(a => int.Parse(a.AccountId)) + 1).ToString("D10");
        newAccount.AccountTypeId = AccountType.FixedTermInvestment;
        newAccount.AccountCraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request)
    {
        var newAccount = _mapper.Map<Account>(request);
        if (CustomerService.FindIfUserExists(request.AccountOwner)) { return Errors.Customer.NotFound; }
        newAccount.AccountId = (accounts.Max(a => int.Parse(a.AccountId)) + 1).ToString("D10");
        newAccount.AccountTypeId = AccountType.Savings;
        newAccount.AccountCraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> GetAccountAsync(string accountId)
    {
        var account = accounts.FirstOrDefault(a => a.AccountId == accountId);
        if (account == null) { return Errors.Account.NotFound; }
        return _mapper.Map<GetAccountResponseDTO>(account);
    }

    public async Task<ErrorOr<List<GetAccountResponseDTO>>> GetListOfAccountsFromOwnerAsync(string accountOwnerId)
    {
        var accountList = accounts.Where(a => a.AccountOwnerId == accountOwnerId).ToList();
        if(accountList == null || accountList.Count == 0) {  return Errors.Account.NotFound; }
        return accountList.Select(c => _mapper.Map<GetAccountResponseDTO>(c)).ToList(); 
    }

    public async Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.AccountId == request.AccountId);

        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountTypeId == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.DepositAmount < 0) { return Errors.Account.AmountValidation; }

        account.AccountBalance += request.DepositAmount;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.AccountId == request.AccountId);
        var targetAccount = accounts.FirstOrDefault(a => a.AccountId == request.TargetAccountId);
        if (account == null || targetAccount == null) { return Errors.Account.NotFound; }
        else if (account.AccountTypeId == AccountType.FixedTermInvestment ||
            targetAccount.AccountTypeId == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.TransferAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.AccountBalance < request.TransferAmount) { return Errors.Account.InsufficientFounds; }

        targetAccount.AccountBalance += request.TransferAmount;
        account.AccountBalance -= request.TransferAmount;

        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.AccountId == request.AccountId);
        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountTypeId == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.WithdawAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.AccountBalance < request.WithdawAmount) { return Errors.Account.InsufficientFounds; }

        account.AccountBalance -= request.WithdawAmount;
        return Result.Updated;
    }
    public async Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId)
    {
        var accountIndex = accounts.FindIndex(a => a.AccountId == accountId);
        if (accountIndex == -1) {  return Errors.Account.NotFound; }
        accounts.RemoveAt(accountIndex);
        
        return Result.Deleted;
    }

}
