using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
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

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found during remove operation.", request.ProductId.Value);
                throw new NotFoundException($"{request.ProductId} id'li ürün bulunamadı");
            }

            _productRepository.Remove(product);
            await _uow.SaveChangesAsync();

            return new RemoveProductCommandResponse()
            {
                Id = product.Id.Value,
                Message = $"{product.Id.Value} id'sine sahip ürün başarıyla silindi."
            };

        }
    }
}
