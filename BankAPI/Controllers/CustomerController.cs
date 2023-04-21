using BankAPI.DTOs.Customer;
using BankAPI.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BankAPI.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// This Service returns the customer for the given Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var getCustomerResponse = await _customerService.GetCustomerAsync(id);
        return getCustomerResponse.Match(
            customer => Ok(customer),
            errors => Problem(errors)
            );
    }

    /// <summary>
    /// This Service returns the customer for the given username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [HttpGet("GetCustomerByUsername/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByUsername(string username)
    {
        var getCustomerByUsername = await _customerService.GetCustomerByUsernameAsync(username);
        return getCustomerByUsername.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service checks if an Id is available
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("isCustomerIdAvailable/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerIdAvailable(string id)
    {
        var getAvailableId = await _customerService.GetIdAvailable(id);
        return getAvailableId.Match(
            available => Ok(available),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service checks if a Username is available
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [HttpGet("isCustomerUsernameAvailable/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerUsernameAvailable(string username)
    {
        var getAvailableUsername = await _customerService.GetUsernameAvailable(username);
        return getAvailableUsername.Match(
            available => Ok(available),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service checks if a email is available
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("isCustomerEmailAvailable/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerEmailAvailable(string email)
    {
        var getAvailableEmail = await _customerService.GetEmailAvailable(email);
        return getAvailableEmail.Match(
            available => Ok(available),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service creates a new customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDTO request)
    {
        var createCustomerRequest = await _customerService.CreateCustomerAsync(request);
        return createCustomerRequest.Match(
            customer => CreatedAtAction(
                actionName: nameof(GetCustomer),
                routeValues: new { id = request.Id },
                value: customer),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's first and last name
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateCustomerInformation/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomerInformation(string id, UpdateCustomerInformationRequestDTO request)
    {
        var updateInformationRequest = await _customerService.UpdateCustomerInformationAsync(id, request);
        return updateInformationRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an exiting customer's username
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateCustomerUsername/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerUsername(string id, UpdateCustomerUsernameRequestDTO request)
    {
        var updateUsernameRequest = await _customerService.UpdateCustomerUsernameAsync(id, request);
        return updateUsernameRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's Email
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateCustomerEmail/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerEmail(string id, UpdateCustomerEmailRequestDTO request)
    {
        var updateEmailRequest = await _customerService.UpdateCustomerEmailAsync(id, request);
        return updateEmailRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("UpdateCustomerId/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerId(string id, UpdateCustomerIdRequestDTO request)
    {
        var updateIdRequest = await _customerService.UpdateCustomerIdAsync(id, request);
        return updateIdRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service updates an existing customer's password
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("updateCustomerPassword/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerPassword(string id, UpdateCustomerPasswordRequestDTO request)
    {
        var updatePasswordRequest = await _customerService.UpdateCustomerPasswordAsync(id, request);
        return updatePasswordRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }

    /// <summary>
    /// This Service deletes an existing customer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var deleteCustomerRequest = await _customerService.DeleteCustomerAsync(id);
        return deleteCustomerRequest.Match(
            customer => NoContent(),
            errors => Problem(errors));
    }
}
