using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Features.Commands.Products.AddBoxToProduct;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.DeleteImage;
using ECommerceAPI.Application.Features.Commands.Products.RemoveBoxFromProduct;
using ECommerceAPI.Application.Features.Commands.Products.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateBoxItem;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Products.UploadImage;
using ECommerceAPI.Application.Features.Queries.Products.GetAllCustomer;
using ECommerceAPI.Application.Features.Queries.Products.GetProductById;
using ECommerceAPI.Application.Repositories.File;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ProductController(IMediator mediator, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IStorageService storageService, IConfiguration config)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne([FromRoute] GetProductByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            ProductDto productDto = await _mediator.Send(request);
            return Ok(productDto);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductCommandRequest request)
        {
            var command = request with { ProductId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("{productId}/boxes")]
        public async Task<IActionResult> Create([FromRoute] Guid productId, [FromBody] AddBoxToProductCommand request)
        {
            var command = request with { ProductId = productId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{productId}/boxes/{boxId}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveBoxFromProductCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{productId}/Boxes/{boxId}")]
        public async Task<IActionResult> UpdateBox([FromRoute] Guid boxId, [FromRoute] Guid productId, [FromBody] UpdateBoxItemCommand request)
        {
            var command = request with { ProductId = productId, BoxId = boxId };
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("[Action]/{productId}")]
        public async Task<IActionResult> Upload([FromRoute] Guid productId, [FromForm] UploadImageCommand request)
        {

            var command = request with { ProductId = productId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("[action]/{productId}/{imageId}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteImageCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);

        }
    }
}


