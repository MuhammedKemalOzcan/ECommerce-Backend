using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateBoxItem
{
    public class UpdateBoxItemCommandHandler : IRequestHandler<UpdateBoxItemCommand>
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateBoxItemCommandHandler> _logger;

        public UpdateBoxItemCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<UpdateBoxItemCommandHandler> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(UpdateBoxItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetByIdOrThrowAsync(new ProductId(request.ProductId));

            product.UpdateBoxItem(new BoxId(request.BoxId), request.Name, request.Quantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Box {BoxId} of Product {ProductId} updated successfully",
                request.BoxId,
                request.ProductId
            );
        }
    }
}
