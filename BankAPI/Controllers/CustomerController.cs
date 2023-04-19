using BankAPI.DTOs.Customer;
using BankAPI.Model;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ErrorOr; 

namespace BankAPI.Controllers;

[Route("api/[controller]")]
public class CustomerController : ApiController
{
    private ICustomerService _customerService;

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
    public async Task<ActionResult> UpdateCustomerInformation(string id, UpdateCustomerInformationRequestDTO request) 
    {
        return Ok(new());
    }

    [HttpPut("UpdateCustomerUsername/{id}")]
    public async Task<ActionResult> UpdateCustomerInformation(string id, UpdateCustomerUsernameRequestDTO requst)
    {
        return Ok(new());
    }

    [HttpPut("UpdateCustomerEmail/{id}")]
    public async Task<ActionResult> UpdateCustomerEmail(string id, UpdateCustomerEmailRequestDTO requst)
    {
        return Ok(new());
    }

    [HttpPut("updateCustomerPassword/{id}")]
    public async Task<ActionResult> UpdateCustomerPassword(string id, UpdateCustomerPasswordRequestDTO request) 
    {
        return Ok(new());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(string id)
    {
        return Ok();
    }
}
