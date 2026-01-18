using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteImageCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdOrThrowAsync(new ProductId(request.ProductId));

            product.DeleteImage(new ImageId(request.imageId));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request.imageId;
        }
    }
}
