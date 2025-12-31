using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Domain.Entities.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetProductById
{
    public record GetProductByIdQueryRequest(Guid Id) : IRequest<ProductDto>;
}
