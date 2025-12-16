using ECommerceAPI.Application.Features.Commands.Customers.AddAddressToCustomer;
using ECommerceAPI.Application.Features.Commands.Customers.DeleteCustomerAddress;
using ECommerceAPI.Application.Features.Commands.Customers.UpdateCustomer;
using ECommerceAPI.Application.Features.Queries.Customer.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressToCustomerCommandRequest request)
        {
            AddAddressToCustomerCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQueryRequest request)
        {
            GetCustomerQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommandRequest request)
        {
            UpdateCustomerCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("address/{addressId}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] DeleteCustomerAddresCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }

}
