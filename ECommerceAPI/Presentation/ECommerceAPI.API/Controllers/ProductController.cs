using ECommerceAPI.Application.Features.Commands.ProductBoxes.CreateProductBox;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.RemoveProductBox;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.UpdateProductBox;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.GetProductBoxes;
using ECommerceAPI.Application.Features.Queries.GetProductById;
using ECommerceAPI.Application.Features.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductQueryRequest request)
        {
            GetProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{ProductId}/boxes")]
        public async Task<IActionResult> Get([FromRoute] GetProductBoxQueryRequest request)
        {
            GetProductBoxQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("{productId}/boxes")]
        public async Task<IActionResult> Create([FromRoute] Guid productId,[FromBody] CreateProductBoxCommandRequest request)
        {
            request.ProductId = productId;
            CreateProductBoxCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("boxes/{BoxId}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductBoxCommandRequest request)
        {
            RemoveProductBoxCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("Boxes")]
        public async Task<IActionResult> UpdateBox([FromBody] UpdateBoxCommandRequest request)
        {
            UpdateBoxCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        
    }
}
