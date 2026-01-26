using ECommerceAPI.Application.Dtos.Cart;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.MergeCarts
{
    public record MergeCartsCommandRequest(Guid? UserId, string? SessionId) : IRequest<CartDto>;
}
