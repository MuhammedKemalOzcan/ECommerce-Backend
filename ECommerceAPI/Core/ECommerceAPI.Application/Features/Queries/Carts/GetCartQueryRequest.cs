using ECommerceAPI.Application.Dtos.Cart;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Carts
{
    public record GetCartQueryRequest(Guid? UserId, string? SessionId) : IRequest<CartDto>;
}
