using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Features.Queries.GetProductById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepo;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepo)
        {
            _productReadRepo = productReadRepo;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Product, object>>[]
            {
                p => p.ProductGalleries,
                p => p.ProductBoxes
            };

            var product = await _productReadRepo.GetByIdAsync(request.Id,includes,true);
            if (product == null) throw new Exception("Ürün bulunamadı");

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Category = product.Category,
                Description = product.Description,
                Features = product.Features,
                Price = product.Price,
                ProductBoxes = product.ProductBoxes.Select(b => new ProductBoxDto { Name = b.Name, Quantity = b.Quantity }).ToList(),
                ProductGalleries = product.ProductGalleries.Select(g => new ProductGalleryDto {Image = g.Image}).ToList()
            };

            return new GetByIdProductQueryResponse
            {
                Product = productDto
            };

        }
    }
}
