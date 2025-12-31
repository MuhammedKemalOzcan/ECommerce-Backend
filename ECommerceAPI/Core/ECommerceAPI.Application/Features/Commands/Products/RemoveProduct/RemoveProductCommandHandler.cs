using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<RemoveProductCommandHandler> _logger;

        public RemoveProductCommandHandler(IProductRepository productRepository, IUnitOfWork uow, ILogger<RemoveProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId));
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found during remove operation.", request.ProductId);
                throw new NotFoundException($"{request.ProductId} id'li ürün bulunamadı");
            }

            _productRepository.Remove(product);
            await _uow.SaveChangesAsync();
        }
    }
}
