using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.ClearCart
{
    public record ClearCartCommandRequest(Guid? UserId, string? SessionId) : IRequest;
}
