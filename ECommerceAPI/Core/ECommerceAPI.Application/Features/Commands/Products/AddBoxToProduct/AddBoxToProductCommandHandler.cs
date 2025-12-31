using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.AddBoxToProduct
{
    public class AddBoxToProductCommandHandler : IRequestHandler<AddBoxToProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AddBoxToProductCommandHandler> _logger;

        public AddBoxToProductCommandHandler(IProductRepository productRepository, IUnitOfWork uow, ILogger<AddBoxToProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddBoxToProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdOrThrowAsync(new ProductId(request.ProductId));

            var newBoxId = new BoxId(Guid.NewGuid());

            product.AddBox(newBoxId, request.Name, request.Quantity);

            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Box '{BoxName}' added to Product {ProductId}.", request.Name, request.ProductId);

            return newBoxId.Value;
        }
    }
}
