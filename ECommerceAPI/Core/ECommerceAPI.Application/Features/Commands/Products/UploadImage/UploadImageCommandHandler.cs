using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Settings;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Application.Features.Commands.Products.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ProductDto>
    {
        private readonly IStorageService _storageService;
        private readonly IProductRepository _productRepository;
        private readonly StorageUrlSettings _storageSettings;
        private readonly IUnitOfWork _unitOfWork;

        public UploadImageCommandHandler(IStorageService storageService, IProductRepository productRepository, IOptions<StorageUrlSettings> options, IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _productRepository = productRepository;
            _storageSettings = options.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId));
            if (product == null)
                throw new NotFoundException($"Product with {request.ProductId} Id cannot found.");

            var files = request.Files.Select(i => i.File).ToList();

            var images = await _storageService.UploadAsync("uploads", files);



            for (int i = 0; i < images.Count; i++)
            {
                var uploadedImage = images[i];
                var originalRequest = request.Files[i];

                if (originalRequest.IsPrimary == true)
                {
                    product.ChangePrimaryImage();
                }

                product.UploadImage(
                    uploadedImage.fileName,
                    uploadedImage.pathOrContainerName,
                    _storageService.StorageName,
                    originalRequest.IsPrimary
                    );
            }

            var productDto = new ProductDto
            {
                Id = product.Id.Value,
                Category = product.Category,
                Description = product.Description,
                Features = product.Features,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ProductBoxes = product.ProductBoxes.Select(b => new ProductBoxDto
                {
                    Id = b.Id.Value,
                    Name = b.Name,
                    Quantity = b.Quantity
                }).ToList(),
                ProductGalleries = product.ProductGalleries.Select(g => new ProductGalleryDto
                {
                    Id = g.Id.Value,
                    FileName = g.FileName,
                    IsPrimary = g.IsPrimary,
                    Path = g.Path,
                }).ToList()
            };

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return productDto;
        }
    }
}
