using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.DeliverOrder
{
    public record DeliverOrderCommand(Guid OrderId) : IRequest;
}