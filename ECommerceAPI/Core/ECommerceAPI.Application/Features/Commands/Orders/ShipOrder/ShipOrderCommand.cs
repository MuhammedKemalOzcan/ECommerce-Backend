using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.ShipOrder
{
    public record ShipOrderCommand(Guid OrderId) : IRequest;
}