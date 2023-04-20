using BankAPI.DTOs.Customer;
using BankAPI.Model;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ErrorOr; 

namespace BankAPI.Controllers;

[Route("api/[controller]")]
public class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var getCustomerResponse = await _customerService.GetCustomerAsync(id);
        return getCustomerResponse.Match(
            customer => Ok(customer),
            errors => Problem(errors) 
            );
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDTO request)
    {
        var createCustomerRequest = await _customerService.CreateCustomerAsync(request);
        return createCustomerRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpPut("UpdateCustomerInformation/{id}")]
    public async Task<IActionResult> UpdateCustomerInformation(string id, UpdateCustomerInformationRequestDTO request) 
    {
        var updateInformationRequest = await _customerService.UpdateCustomerInformationAsync(id, request);
        return updateInformationRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpPut("UpdateCustomerUsername/{id}")]
    public async Task<IActionResult> UpdateCustomerUsername(string id, UpdateCustomerUsernameRequestDTO request)
    {
        var updateUsernameRequest = await _customerService.UpdateCustomerUsernamAsync(id, request);
        return updateUsernameRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpPut("UpdateCustomerEmail/{id}")]
    public async Task<IActionResult> UpdateCustomerEmail(string id, UpdateCustomerEmailRequestDTO request)
    {
        var updateEmailRequest = await _customerService.UpdateCustomerEmailAsync(id, request);
        return updateEmailRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpPut("UpdateCustomerId/{id}")]
    public async Task<IActionResult> UpdateCustomerId(string id, UpdateCustomerIdRequestDTO request)
    {
        var updateIdRequest = await _customerService.UpdateCustomerIdAsync(id, request);
        return updateIdRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpPut("updateCustomerPassword/{id}")]
    public async Task<IActionResult> UpdateCustomerPassword(string id, UpdateCustomerPasswordRequestDTO request) 
    {
        var updatePasswordRequest = await _customerService.UpdateCustomerPasswordAsync(id, request);
        return updatePasswordRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var deleteCustomerRequest = await _customerService.DeleteCustomerAsync(id);
        return deleteCustomerRequest.Match(
            customer => Ok(customer),
            errors => Problem(errors));
    }
}
