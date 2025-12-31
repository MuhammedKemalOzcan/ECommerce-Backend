using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.CustomerCommand.AddAddress;
using ECommerceAPI.Application.Features.Commands.CustomerCommand.PrimaryAddress;
using ECommerceAPI.Application.Features.Commands.CustomerCommand.RemoveAddress;
using ECommerceAPI.Application.Features.Queries.Customer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid appUserId))
            {
                throw new UnauthorizedException("Invalid token");
            }
            ;

            var request = new GetCustomerQueryRequest(appUserId);

            var result = await _mediator.Send(request);

            return Ok(result);

        }

        [HttpPost("Address")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressToCustomerCommandRequest request)
        {
            Guid customerAddresId = await _mediator.Send(request);

            return Ok(customerAddresId);
        }

        [HttpDelete("Address/{customerAddressId}")]
        public async Task<IActionResult> RemoveAddress([FromRoute] Guid customerAddressId)
        {
            var request = new RemoveAddressCommand(new CustomerAddressId(customerAddressId));
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut("SetPrimaryAddress/{customerAddressId}")]
        public async Task<IActionResult> SetPrimaryAddress([FromRoute] Guid customerAddressId)
        {
            var request = new SetPrimaryAddressCommand(new CustomerAddressId(customerAddressId));
            await _mediator.Send(request);

            return Ok();
        }
    }

}



