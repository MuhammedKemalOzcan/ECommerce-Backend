using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork uow, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                _logger.LogWarning($"{request.ProductId} cannot found");
                throw new NotFoundException($"{request.ProductId} cannot found");
            }

            product.Update(request.Name, request.Stock, request.Price, request.Category, request.Description, request.Features);

            await _uow.SaveChangesAsync(cancellationToken);

        }
    }
}
