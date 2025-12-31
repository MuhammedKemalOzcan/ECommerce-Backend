using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Domain.Entities.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public record UpdateProductCommandRequest(Guid ProductId, string Name, decimal Price, string Category, int Stock, string Description, string Features) : IRequest<UpdateProductDto>;

}
