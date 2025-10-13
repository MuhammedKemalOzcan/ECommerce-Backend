using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Features.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, GetProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepo;

        public GetProductQueryHandler(IProductReadRepository productReadRepo)
        {
            _productReadRepo = productReadRepo;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Product, object>>[]
            {
                p => p.ProductGalleries,
                p => p.ProductBoxes
            };

            var products = await _productReadRepo.GetAllAsync(includes, null, true, cancellationToken);

            var productDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price,
                Features = p.Features,
                Description = p.Description,
                ProductBoxes = p.ProductBoxes.Select(b => new ProductBoxDto {Name = b.Name,Quantity = b.Quantity }).ToList(),
                ProductGalleries = p.ProductGalleries.Select(g => new ProductGalleryDto {Image = g.Image}).ToList()
            }).ToList();

            return new GetProductQueryResponse
            {
                Products = productDto
            };

        }
    }
}
