using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Dtos.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllCustomer
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<ProductDto>>
    {
        private readonly IEcommerceAPIDbContext _context;

        public GetAllProductsQueryHandler(IEcommerceAPIDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    Id = p.Id.Value,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    Category = p.Category,
                    Description = p.Description,
                    Features = p.Features,


                    ProductBoxes = p.ProductBoxes.Select(b => new ProductBoxDto
                    {
                        Id = b.Id.Value,
                        Name = b.Name,
                        Quantity = b.Quantity
                    }).ToList(),


                    ProductGalleries = p.ProductGalleries
                        .Select(g => new ProductGalleryDto
                        {
                            Id = g.Id.Value,
                            FileName = g.FileName,
                            Path = g.Path,
                            IsPrimary = g.IsPrimary
                        }).ToList()
                })
                .OrderByDescending(p => p.Price)
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}