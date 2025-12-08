using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.CreateProductBox;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.RemoveProductBox;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.UpdateProductBox;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.GetProductBoxes;
using ECommerceAPI.Application.Features.Queries.GetProductById;
using ECommerceAPI.Application.Features.Queries.GetProducts;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.ProductGallery;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductGalleryReadRepository _galleryReadRepository;
        private readonly IProductGalleryWriteRepository _galleryWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IConfiguration _config;

        public ProductController(IMediator mediator, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductGalleryReadRepository galleryReadRepository, IProductGalleryWriteRepository galleryWriteRepository, IStorageService storageService, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IConfiguration config)
        {
            _mediator = mediator;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _galleryReadRepository = galleryReadRepository;
            _galleryWriteRepository = galleryWriteRepository;
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _config = config;
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

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("Boxes/{boxId}")]
        public async Task<IActionResult> UpdateBox([FromRoute] Guid boxId, [FromBody] UpdateBoxCommandRequest request)
        {
            request.BoxId = boxId;
            UpdateBoxCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[Action]/{productId}")]
        public async Task<IActionResult> Upload([FromRoute] Guid productId ){

            Product product = await _productReadRepository.GetByIdAsync(productId,true);
            if (product == null) throw new Exception("Ürün bulunamadı");

            _productWriteRepository.Attach(product);

            var datas = await _storageService.UploadAsync("uploads", Request.Form.Files);

            int? primaryIndex = null;
            if (int.TryParse(Request.Form["primaryIndex"], out var idx))
                primaryIndex = idx;

            // Eğer kullanıcı birincil seçtiyse, mevcut birincilleri sıfırla
            if (primaryIndex.HasValue)
            {
                var primary = await _galleryReadRepository.FirstOrDefaultAsync(g => g.IsPrimary && g.Product.Any(p => p.Id == productId && g.IsPrimary), CancellationToken.None, false);

                if (primary is not null)
                {
                    primary.IsPrimary = false;
                    await _galleryWriteRepository.SaveChangesAsync();
                }
                    
            }

            await _galleryWriteRepository.AddRangeAsync(datas.Select((d,i) => new ProductGallery
            {
                FileName = d.fileName,
                Path = $"{_config["BaseStorageUrl"]}/{d.pathOrContainerName}",
                Storage = _storageService.StorageName,
                IsPrimary = primaryIndex.HasValue && primaryIndex.Value == i,
                Product = new List<Product>() { product }
            }).ToList());
            await _galleryWriteRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("[action]/{productId}/{imageId}")]
        public async Task<IActionResult> DeleteProductImage(Guid productId, Guid imageId)
        {
            var product = await _productWriteRepository.Table
            .Include(p => p.ProductGalleries)
            .FirstOrDefaultAsync(p => p.Id == productId);

            ProductGallery? deletingImage =  product.ProductGalleries.FirstOrDefault(p => p.Id == imageId);

            product.ProductGalleries?.Remove(deletingImage);
            await _productWriteRepository.SaveChangesAsync();
            return Ok();

        }




    }
}
