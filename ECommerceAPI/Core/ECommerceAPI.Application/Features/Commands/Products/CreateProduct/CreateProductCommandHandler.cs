using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ProductDto>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IUnitOfWork uow)
        {
            _logger = logger;
            _productRepository = productRepository;
            _uow = uow;
        }

        public async Task<ProductDto> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Stock, request.Price, request.Category, request.Description, request.Features);

            _logger.LogInformation("Product created with ID: {ProductId}", product.Id.Value);

            if (request.ProductBoxes != null)
            {
                foreach (var box in request.ProductBoxes)
                {
                    // Value Object ID üretimi
                    var newBoxId = new BoxId(Guid.NewGuid());
                    // Domain metodunu çağır
                    product.AddBox(newBoxId, box.Name, box.Quantity);
                }
            }

            _productRepository.Add(product);
             await _uow.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id.Value,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Category = product.Category,
                Description = product.Description,
                Features = product.Features,
                ProductBoxes = product.ProductBoxes.Select(b => new ProductBoxDto
                {
                    Id = b.Id.Value,
                    Name = b.Name,
                    Quantity = b.Quantity
                }).ToList()
            };
        }
    }
}
