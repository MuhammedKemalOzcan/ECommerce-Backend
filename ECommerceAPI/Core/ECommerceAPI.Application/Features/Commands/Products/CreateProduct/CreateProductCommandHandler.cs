using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepo;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepo)
        {
            _productWriteRepo = productWriteRepo;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            //Gelen Requestten bir product nesnesi oluşturulur.
            var product = new Product
            {
                Name = request.Name,
                Stock = request.Stock,
                Category = request.Category,
                Price = request.Price,
                Description = request.Description,
                Features = request.Features
            };

            if(request.GalleryUrls is { Count: > 0 })
            {
                foreach (var url in request.GalleryUrls.Where(u => !string.IsNullOrEmpty(u)))
                {
                    product.ProductGalleries.Add(new ProductGallery { Image = url });
                }
            };

            if (request.BoxItems is { Count: > 0 })
            {
                foreach (var item in request.BoxItems)
                {
                    if (string.IsNullOrEmpty(item.Name)) throw new ValidationException("Box item name is required");
                    if (item.Quantity <= 0) throw new ValidationException("Item quantity must be greater than zero");

                    product.ProductBoxes.Add(new ProductBox { Name = item.Name, Quantity = item.Quantity });
                }
            };



            await _productWriteRepo.AddAsync(product,cancellationToken);
            await _productWriteRepo.SaveChangesAsync();

            return new CreateProductCommandResponse();

        }
    }
}
