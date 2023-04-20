using BankAPI.DTOs.Account;
using BankAPI.Services.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Principal;

namespace BankAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
            var getAccount = await _accountService.GetAccountAsync(id);
            return getAccount.Match(
                account => Ok(account),
                errors => Problem(errors));
        }

        /// <summary>
        /// This Services returns the all the accounts form the 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("getAllAccountsFromCustomer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAccountsFromCustomer(string CustomerId)
        {
            var getAllAccounts = await _accountService.GetListOfAccountsFromOwnerAsync(CustomerId);
            return getAllAccounts.Match(
                allAccounts => Ok(allAccounts),
                errors => Problem(errors));
        }

        /// <summary>
        /// This Service creates a new checking account 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("createCheckingAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCheckingAccount(CreateCheckingAccountRequestDTO request)
        {
            var createCheckingAccount = await _accountService.CreateChekingAccountAsync(request);
            return createCheckingAccount.Match(
                account => CreatedAtAction(
                    actionName: nameof(GetAccount),
                    routeValues: new { id = account.AccountId },
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
            var createSavingsAccount = await _accountService.CreateSavingsAccountAsync(request);
            return createSavingsAccount.Match(
                account => CreatedAtAction(
                    actionName: nameof(GetAccount),
                    routeValues: new { id = account.AccountId },
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
            var createFixedTermInvestmentAccount = await _accountService.CreateFixedTermInvestmentAccountAsync(request);
            return createFixedTermInvestmentAccount.Match(
                account => CreatedAtAction(
                    actionName: nameof(GetAccount),
                    routeValues: new { id = account.AccountId },
                    value: account),
                errors => Problem(errors));
        }

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
            var updateWithdraw = await _accountService.UpdateWithdrawBalanceAsync(request);
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
            var updateDeposit = await _accountService.UpdateDepositBalanceAsync(request);
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
            var updateTransfer = await _accountService.UpdateTransferBalanceAsync(request);
            return updateTransfer.Match(
                account => NoContent(),
                errors => Problem(errors));
        }
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
            var deleteAccount = await _accountService.DeleteAccountAsync(id);
            return deleteAccount.Match(
                account => NoContent(),
                errors => Problem(errors));
        }
    }
}
