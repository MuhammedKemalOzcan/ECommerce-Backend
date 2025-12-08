using ECommerceAPI.Application.Features.Commands.Customers.AddAddressToCustomer;
using ECommerceAPI.Application.Features.Commands.Customers.CreateCustomer;
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

        [HttpPost("{CustomerId}/addresses")]
        public async Task<IActionResult> AddAddress([FromRoute] Guid CustomerId, [FromBody] AddAddressToCustomerCommandRequest request)
        {
            AddAddressToCustomerCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommandRequest request)
        {
            CreateCustomerCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }

}
