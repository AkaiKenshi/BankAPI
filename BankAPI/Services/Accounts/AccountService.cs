using AutoMapper;
using BankAPI.Data.Model;
using BankAPI.ServiceErrors;
using ErrorOr;
using BankAPI.Data;
using Microsoft.EntityFrameworkCore;
using BankAPI.Contracts.DTOs.Accounts;

namespace BankAPI.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly BankDataContext _context;

    public AccountService(IMapper mapper, BankDataContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    //Create     
    public async Task<ErrorOr<GetAccountResponseDTO>> CreateChekingAccountAsync(CreateCheckingAccountRequestDTO request, string customerId)
    {
        var getGenerateAccount = await GenerateAccountAsync(request.Balance, customerId);
        if (getGenerateAccount.IsError) { return getGenerateAccount.FirstError; }

        var newAccount = getGenerateAccount.Value;
        newAccount.AccountType = AccountType.Checking;

        await _context.AddAsync(newAccount);
        _context.SaveChanges();

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request, string customerId)
    {
        var getGenerateAccount = await GenerateAccountAsync(request.Balance, customerId);
        if (getGenerateAccount.IsError) { return getGenerateAccount.FirstError; }

        var newAccount = getGenerateAccount.Value;
        newAccount.AccountType = AccountType.FixedTermInvestment;
        newAccount.CraetedDate = DateOnly.FromDateTime(DateTime.Now);
        newAccount.Term = request.Term;

        await _context.AddAsync(newAccount);
        _context.SaveChanges();

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request, string customerId)
    {
        var getGenerateAccount = await GenerateAccountAsync(request.Balance, customerId);
        if (getGenerateAccount.IsError) { return getGenerateAccount.FirstError; }

        var newAccount = getGenerateAccount.Value;
        newAccount.AccountType = AccountType.Savings;
        newAccount.CraetedDate = DateOnly.FromDateTime(DateTime.Now);

        await _context.AddAsync(newAccount);
        _context.SaveChanges();

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }


    //Get
    public async Task<ErrorOr<GetAccountResponseDTO>> GetAccountAsync(string accountId, string customerId)
    {
        var account = await _context.Accounts.Include(c => c.Customer)
            .FirstOrDefaultAsync(a => a.Id == accountId);

        if (account == null) { return Errors.Account.NotFound; }
        else if (account.Customer.Id != customerId) { return Errors.Account.UnauthorizedAccountAccess; }


        return _mapper.Map<GetAccountResponseDTO>(account);
    }

    public async Task<ErrorOr<List<GetAccountResponseDTO>>> GetListOfAccountsFromOwnerAsync(string accountOwnerId)
    {
        var accountList = await _context.Accounts.Include(c => c.Customer)
            .Where(a => a.Customer.Id == accountOwnerId).ToListAsync();
        if (accountList == null || accountList.Count == 0) { return Errors.Account.NotFound; }
        return accountList.Select(c => _mapper.Map<GetAccountResponseDTO>(c)).ToList();
    }


    //Update
    public async Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request, string customerId)
    {
        var account = await _context.Accounts.Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.DepositAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.Customer.Id != customerId) { return Errors.Account.UnauthorizedAccountAccess; }

        account.Balance += request.DepositAmount;

        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request, string customerId)
    {
        var account = await _context.Accounts.Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == request.Id);
        var targetAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == request.TargetAccountId);

        if (account == null || targetAccount == null) { return Errors.Account.NotFound; }
        else if (request.Id == request.TargetAccountId) { return Errors.Account.InvalidTargetAccount; }
        else if (account.AccountType == AccountType.FixedTermInvestment ||
            targetAccount.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.TransferAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.Balance < request.TransferAmount) { return Errors.Account.InsufficientFounds; }
        else if (account.Customer.Id != customerId) { return Errors.Account.UnauthorizedAccountAccess; }

        targetAccount.Balance += request.TransferAmount;
        account.Balance -= request.TransferAmount;

        _context.SaveChanges();
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request, string customerId)
    {
        var account = await _context.Accounts.Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.WithdawAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.Balance < request.WithdawAmount) { return Errors.Account.InsufficientFounds; }
        else if (account.Customer.Id != customerId) { return Errors.Account.UnauthorizedAccountAccess; }

        account.Balance -= request.WithdawAmount;
        _context.SaveChanges();
        return Result.Updated;
    }


    //Delete
    public async Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId, string customerId)
    {
        var account = await _context.Accounts.Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == accountId);
        if (account == null) { return Errors.Account.NotFound; }

        _context.Accounts.Remove(account);
        _context.SaveChanges();

        return Result.Deleted;
    }

    #region Helper Functions
    private async Task<ErrorOr<Account>> GenerateAccountAsync(double balance, string ownerID)
    {
        var newAccount = new Account();
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == ownerID);
        if (customer == null) { return Errors.Customer.NotFound; }
        else if (balance < 0) { return Errors.Account.AmountValidation; }
        newAccount.Balance = balance;
        newAccount.Customer = customer;
        newAccount.CraetedDate = (await _context.Time.FirstOrDefaultAsync())!.CurrentDate;
        return newAccount;
    }
    #endregion
}
