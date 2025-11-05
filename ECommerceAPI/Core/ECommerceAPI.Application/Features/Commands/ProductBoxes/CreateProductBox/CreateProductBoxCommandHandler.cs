using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.CreateProductBox
{
    public class CreateProductBoxCommandHandler : IRequestHandler<CreateProductBoxCommandRequest, CreateProductBoxCommandResponse>
    {
        private readonly IProductBoxWriteRepository _productBoxWriteRepository;
        private readonly IProductBoxReadRepository _productBoxReadRepository;
        private readonly IProductWriteRepository _productWriteRepo;
        private readonly IProductReadRepository _productReadRepo;

        public CreateProductBoxCommandHandler(IProductBoxWriteRepository productBoxWriteRepository, IProductBoxReadRepository productBoxReadRepository, IProductWriteRepository productWriteRepo, IProductReadRepository productReadRepo)
        {
            _productBoxWriteRepository = productBoxWriteRepository;
            _productBoxReadRepository = productBoxReadRepository;
            _productWriteRepo = productWriteRepo;
            _productReadRepo = productReadRepo;
        }

        public async Task<CreateProductBoxCommandResponse> Handle(CreateProductBoxCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepo.GetByIdAsync(request.ProductId,true);
            if (product == null) throw new NotFoundException("Ürün Bulunamadı");

            var productBox = new ProductBox
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Quantity = request.Quantity
            };

            await _productBoxWriteRepository.AddAsync(productBox);
            await _productBoxWriteRepository.SaveChangesAsync();

            return new CreateProductBoxCommandResponse
            {
                Name = productBox.Name,
                Quantity = productBox.Quantity
            };


        }
    }
}
