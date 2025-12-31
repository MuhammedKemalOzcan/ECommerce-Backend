using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Guid>
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

        public async Task<Guid> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Stock, request.Price, request.Category, request.Description, request.Features);

            _logger.LogInformation("Product created with ID: {ProductId}", product.Id.Value);

            _productRepository.Add(product);
            await _uow.SaveChangesAsync();

            return product.Id.Value;
        }
    }
}
