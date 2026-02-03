using ECommerceAPI.Application.Features.Commands.Orders.CreateOrder;
using ECommerceAPI.Application.Features.Queries.Orders.GetOne;
using ECommerceAPI.Application.Features.Queries.Orders.ListOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var query = new GetOrdersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOneOrder([FromRoute] GetByIdOrdersQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] InitializePaymentCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
