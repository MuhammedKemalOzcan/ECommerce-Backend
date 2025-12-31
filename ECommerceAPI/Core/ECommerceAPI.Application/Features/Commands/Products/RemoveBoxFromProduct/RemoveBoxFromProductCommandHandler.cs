using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveBoxFromProduct
{
    public class RemoveBoxFromProductCommandHandler : IRequestHandler<RemoveBoxFromProductCommand>
    {
        private readonly ILogger<RemoveBoxFromProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public RemoveBoxFromProductCommandHandler(IProductRepository productRepository, IUnitOfWork uow, ILogger<RemoveBoxFromProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task Handle(RemoveBoxFromProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdOrThrowAsync(new ProductId(request.ProductId));

            product.RemoveBox(new BoxId(request.BoxId));

            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Box item with {request.ProductId} Id removed");

        }
    }
}
