using ECommerceAPI.Application.Dtos.Cart;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem
{
    public record UpdateCartItemsCommandRequest(
        Guid? UserId,
        string? SessionId,
        Guid CartItemId,
        int Quantity)
        : IRequest<CartDto>;
}
