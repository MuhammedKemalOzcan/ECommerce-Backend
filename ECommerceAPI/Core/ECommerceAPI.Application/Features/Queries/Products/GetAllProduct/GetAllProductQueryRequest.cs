using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Domain.Entities.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllCustomer
{
    public record GetAllProductQueryRequest() : IRequest<List<ProductDto>>;
}
