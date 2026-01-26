using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.RemoveCartItem
{
    public record RemoveCartItemRequest(Guid? UserId, string? SessionId, Guid CartItemId) 
        : IRequest;

}
