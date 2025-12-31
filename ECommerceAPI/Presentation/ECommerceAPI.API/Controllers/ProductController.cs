using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Products.AddBoxToProduct;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.RemoveBoxFromProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetAllCustomer;
using ECommerceAPI.Application.Features.Queries.Products.GetProductById;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _config;

        public ProductController(IMediator mediator, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IStorageService storageService, IConfiguration config)
        {
            _mediator = mediator;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _storageService = storageService;
            _config = config;
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
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
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

        //[HttpDelete("{Id}")]
        //public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        //{
        //    RemoveProductCommandResponse response = await _mediator.Send(request);
        //    return Ok(response);
        //}

        //[HttpPut("Boxes/{boxId}")]
        //public async Task<IActionResult> UpdateBox([FromRoute] Guid boxId, [FromBody] UpdateBoxCommandRequest request)
        //{
        //    request.BoxId = boxId;
        //    UpdateBoxCommandResponse response = await _mediator.Send(request);
        //    return Ok(response);
        //}

        //[HttpPost("[Action]/{productId}")]
        //public async Task<IActionResult> Upload([FromRoute] Guid productId ){

        //    Product product = await _productReadRepository.GetByIdAsync(productId,true);
        //    if (product == null) throw new Exception("Ürün bulunamadı");

        //    _productWriteRepository.Attach(product);

        //    var datas = await _storageService.UploadAsync("uploads", Request.Form.Files);

        //    int? primaryIndex = null;
        //    if (int.TryParse(Request.Form["primaryIndex"], out var idx))
        //        primaryIndex = idx;

        //    // Eğer kullanıcı birincil seçtiyse, mevcut birincilleri sıfırla
        //    if (primaryIndex.HasValue)
        //    {
        //        var primary = await _galleryReadRepository.FirstOrDefaultAsync(g => g.IsPrimary && g.Product.Any(p => p.Id == productId && g.IsPrimary), CancellationToken.None, false);

        //        if (primary is not null)
        //        {
        //            primary.IsPrimary = false;
        //            await _galleryWriteRepository.SaveChangesAsync();
        //        }

        //    }

        //    await _galleryWriteRepository.AddRangeAsync(datas.Select((d,i) => new ProductGallery
        //    {
        //        FileName = d.fileName,
        //        Path = $"{_config["BaseStorageUrl"]}/{d.pathOrContainerName}",
        //        Storage = _storageService.StorageName,
        //        IsPrimary = primaryIndex.HasValue && primaryIndex.Value == i,
        //        Product = new List<Product>() { product }
        //    }).ToList());
        //    await _galleryWriteRepository.SaveChangesAsync();
        //    return Ok();
        //}

        //[HttpDelete("[action]/{productId}/{imageId}")]
        //public async Task<IActionResult> DeleteProductImage(Guid productId, Guid imageId)
        //{
        //    var product = await _productWriteRepository.Table
        //    .Include(p => p.ProductGalleries)
        //    .FirstOrDefaultAsync(p => p.Id == productId);

        //    ProductGallery? deletingImage =  product.ProductGalleries.FirstOrDefault(p => p.Id == imageId);

        //    product.ProductGalleries?.Remove(deletingImage);
        //    await _productWriteRepository.SaveChangesAsync();
        //    return Ok();

        //}




    }
}
