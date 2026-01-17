using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ProductDto>
    {
        private readonly IEcommerceAPIDbContext _context;

        public GetProductByIdQueryHandler(IEcommerceAPIDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == new ProductId(request.Id))
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
                }).FirstOrDefaultAsync(cancellationToken);

            if (product == null) throw new NotFoundException($"Product with ID {request.Id} not found.");

            return product;
        }
    }
}