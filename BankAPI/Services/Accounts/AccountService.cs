﻿using AutoMapper;
using BankAPI.DTOs.Account;
using BankAPI.Data.Model;
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
            Id = "0000000001",
            Balance = 100,
            AccountType = AccountType.Checking,
            Customer = CustomerService.customers[0],
            CraetedDate = new DateOnly(2023,4,20)
        },
        new Account
        {
            Id = "0000000002",
            Balance = 300,
            Customer = CustomerService.customers[1],
            AccountType = AccountType.Checking,
            CraetedDate = new DateOnly(2023,4,20)
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
        if(CustomerService.FindIfUserExists(request.OwnerId)) { return Errors.Customer.NotFound; }
        newAccount.Id = (accounts.Max(a => int.Parse(a.Id)) + 1).ToString("D10");
        newAccount.AccountType = AccountType.Checking;
        newAccount.CraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateFixedTermInvestmentAccountAsync(CreateFixedTermInvestmentAccountRequestDTO request)
    {
        var newAccount = _mapper.Map<Account>(request);
        if (CustomerService.FindIfUserExists(request.OwnerId)) { return Errors.Customer.NotFound; }
        newAccount.Id = (accounts.Max(a => int.Parse(a.Id)) + 1).ToString("D10");
        newAccount.AccountType = AccountType.FixedTermInvestment;
        newAccount.CraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> CreateSavingsAccountAsync(CreateSavingsAccountRequestDTO request)
    {
        var newAccount = _mapper.Map<Account>(request);
        if (CustomerService.FindIfUserExists(request.OwnerId)) { return Errors.Customer.NotFound; }
        newAccount.Id = (accounts.Max(a => int.Parse(a.Id)) + 1).ToString("D10");
        newAccount.AccountType = AccountType.Savings;
        newAccount.CraetedDate = DateOnly.FromDateTime(DateTime.Now);

        return _mapper.Map<GetAccountResponseDTO>(newAccount);
    }

    public async Task<ErrorOr<GetAccountResponseDTO>> GetAccountAsync(string accountId)
    {
        var account = accounts.FirstOrDefault(a => a.Id == accountId);
        if (account == null) { return Errors.Account.NotFound; }
        return _mapper.Map<GetAccountResponseDTO>(account);
    }

    public async Task<ErrorOr<List<GetAccountResponseDTO>>> GetListOfAccountsFromOwnerAsync(string accountOwnerId)
    {
        var accountList = accounts.Where(a => a.Customer.Id == accountOwnerId).ToList();
        if(accountList == null || accountList.Count == 0) {  return Errors.Account.NotFound; }
        return accountList.Select(c => _mapper.Map<GetAccountResponseDTO>(c)).ToList(); 
    }

    public async Task<ErrorOr<Updated>> UpdateDepositBalanceAsync(UpdateDepositBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.Id == request.Id);

        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.DepositAmount < 0) { return Errors.Account.AmountValidation; }

        account.Balance += request.DepositAmount;
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateTransferBalanceAsync(UpdateTransferBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.Id == request.Id);
        var targetAccount = accounts.FirstOrDefault(a => a.Id == request.TargetAccountId);
        if (account == null || targetAccount == null) { return Errors.Account.NotFound; }
        else if (account.AccountType == AccountType.FixedTermInvestment ||
            targetAccount.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.TransferAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.Balance < request.TransferAmount) { return Errors.Account.InsufficientFounds; }

        targetAccount.Balance += request.TransferAmount;
        account.Balance -= request.TransferAmount;

        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateWithdrawBalanceAsync(UpdateWithdrawBalanceRequestDTO request)
    {
        var account = accounts.FirstOrDefault(a => a.Id == request.Id);
        if (account == null) { return Errors.Account.NotFound; }
        else if (account.AccountType == AccountType.FixedTermInvestment) { return Errors.Account.InvalidAction; }
        else if (request.WithdawAmount < 0) { return Errors.Account.AmountValidation; }
        else if (account.Balance < request.WithdawAmount) { return Errors.Account.InsufficientFounds; }

        account.Balance -= request.WithdawAmount;
        return Result.Updated;
    }
    public async Task<ErrorOr<Deleted>> DeleteAccountAsync(string accountId)
    {
        var accountIndex = accounts.FindIndex(a => a.Id == accountId);
        if (accountIndex == -1) {  return Errors.Account.NotFound; }
        accounts.RemoveAt(accountIndex);
        
        return Result.Deleted;
    }

}
