using BankAPI.Data.Model;
using BankAPI.DTOs.Customers;
using BankAPI.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace BankAPI.Controllers;


public class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    #region Get

    /// <summary>
    /// This Service checks if an Id is available
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("isCustomerIdAvailable/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerIdAvailable(string id) => Ok(await _customerService.GetIdAvailable(id));

    /// <summary>
    /// This Service checks if a Username is available
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [HttpGet("isCustomerUsernameAvailable/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerUsernameAvailable(string username) => Ok(await _customerService.GetUsernameAvailable(username));

    /// <summary>
    /// This Service checks if a email is available
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("isCustomerEmailAvailable/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerEmailAvailable(string email) => Ok(await _customerService.GetEmailAvailable(email));

    /// <summary>
    /// This Service checks if the password is a valid password 
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("isValidPassword/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetValidPassword(string password) => Ok(_customerService.GetVaildPassword(password));

    #endregion
    #region Post

    /// <summary>
    /// This Service creates a new customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDTO request)
    {
        var createCustomerRequest = await _customerService.CreateCustomerAsync(request);
        return createCustomerRequest.Match(
            customer => CreatedAtAction(
                actionName: nameof(CreateCustomer),
                routeValues: new { id = request.Id },
                value: customer),
            errors => Problem(errors));
    }


    /// <summary>
    /// This Service gets the account login
    /// </summary>
    /// <param name="getRequest"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginCustomer(GetCustomerLoginRequestDTO getRequest)
    {
        var getLoginRequest = await _customerService.GetLoginCustomer(getRequest);

        return getLoginRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));

    }

    #endregion
    #region update 

    /// <summary>
    /// This Service updates an existing customer's first and last name
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("UpdateCustomerInformation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomerInformation(UpdateCustomerInformationRequestDTO request)
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;  
        var updateInformationRequest = await _customerService.UpdateCustomerInformationAsync(id, request);
        return updateInformationRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an exiting customer's username
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("UpdateCustomerUsername")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerUsername(UpdateCustomerUsernameRequestDTO request)
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var updateUsernameRequest = await _customerService.UpdateCustomerUsernameAsync(id, request);
        return updateUsernameRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's Email
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("UpdateCustomerEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerEmail(UpdateCustomerEmailRequestDTO request)
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var updateEmailRequest = await _customerService.UpdateCustomerEmailAsync(id, request);
        return updateEmailRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]    
    [HttpPut("updateCustomerPassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerPassword(UpdateCustomerPasswordRequestDTO request)
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var updatePasswordRequest = await _customerService.UpdateCustomerPasswordAsync(id, request);
        return updatePasswordRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    #endregion
    #region delete

    /// <summary>
    /// This Service deletes an existing customer
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer()
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var deleteCustomerRequest = await _customerService.DeleteCustomerAsync(id);
        return deleteCustomerRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }
    #endregion
}
