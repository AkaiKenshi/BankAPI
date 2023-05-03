using BankAPI.Contracts.DTOs.Accounts;
using BankAPI.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace BankAPI.Controllers;

[Authorize]

public class AccountController : ApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }


    #region get
    /// <summary>
    /// this service checks if an account exists 
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet("checkAccountExists/{accountId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetCheckIfAccountExits(string accountId) => Ok(await _accountService.GetAccountExists(accountId));

    /// <summary>
    /// This service returns the account from the given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccount(string id)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var getAccount = await _accountService.GetAccountAsync(id, customerId);
        return getAccount.Match(
            account => Ok(account),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Services returns the all the accounts form the user
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    [HttpGet("getAllAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAccounts()
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var getAllAccounts = await _accountService.GetListOfAccountsFromOwnerAsync(customerId);
        return getAllAccounts.Match(
            allAccounts => Ok(allAccounts),
            errors => Problem(errors));
    }

    #endregion
    #region Post

    /// <summary>
    /// This Service creates a new checking account 
    /// </summary>
    /// <param name="request"></param>
    /// <returns> </returns>
    [HttpPost("createCheckingAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCheckingAccount(CreateCheckingAccountRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var createCheckingAccount = await _accountService.CreateChekingAccountAsync(request, customerId);
        return createCheckingAccount.Match(
            account => CreatedAtAction(
                actionName: nameof(GetAccount),
                routeValues: new { id = account.Id },
                value: account),
            errors => Problem(errors));
    }
    
    /// <summary>
    /// This Service creates a new savings account 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("createSavingsAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSavingsAccount(CreateSavingsAccountRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var createSavingsAccount = await _accountService.CreateSavingsAccountAsync(request, customerId);
        return createSavingsAccount.Match(
            account => CreatedAtAction(
                actionName: nameof(GetAccount),
                routeValues: new { id = account.Id },
                value: account),
            errors => Problem(errors));
    }
    
    /// <summary>
    /// This Service creates a new fixed term investment account 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("createFixedTermInvestmentAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateFixedTermInvestmentAccount(CreateFixedTermInvestmentAccountRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var createFixedTermInvestmentAccount = await _accountService.CreateFixedTermInvestmentAccountAsync(request, customerId);
        return createFixedTermInvestmentAccount.Match(
            account => CreatedAtAction(
                actionName: nameof(GetAccount),
                routeValues: new { id = account.Id },
                value: account),
            errors => Problem(errors));
    }

    #endregion
    #region update

    /// <summary>
    /// This Service is used to withdraw money from a an account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateWithdrawBalance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWithdrawBalance(UpdateWithdrawBalanceRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var updateWithdraw = await _accountService.UpdateWithdrawBalanceAsync(request, customerId);
        return updateWithdraw.Match(
            account => NoContent(),
            errors => Problem(errors));
    }
 
    /// <summary>
    /// This Service is used to deposit money from a an account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateDepositBalance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDepositBalance(UpdateDepositBalanceRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var updateDeposit = await _accountService.UpdateDepositBalanceAsync(request, customerId);
        return updateDeposit.Match(
            account => NoContent(),
            errors => Problem(errors));
    }
 
    /// <summary>
    /// This Service is used to transfer money from a an account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateTransferBalance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTransferBalance(UpdateTransferBalanceRequestDTO request)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var updateTransfer = await _accountService.UpdateTransferBalanceAsync(request, customerId);
        return updateTransfer.Match(
            account => NoContent(),
            errors => Problem(errors));
    }

    #endregion
    #region delete

    /// <summary>
    /// This Service is used to delete an account 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAccount(string id)
    {
        var customerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        var deleteAccount = await _accountService.DeleteAccountAsync(id, customerId);
        return deleteAccount.Match(
            account => NoContent(),
            errors => Problem(errors));
    }

    #endregion
}
