using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId) : IRequest;
}