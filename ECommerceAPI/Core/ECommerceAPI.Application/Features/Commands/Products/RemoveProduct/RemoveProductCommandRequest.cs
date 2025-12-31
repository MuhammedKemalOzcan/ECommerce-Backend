using ECommerceAPI.Domain.Entities.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public record RemoveProductCommandRequest(Guid ProductId) : IRequest;
}
