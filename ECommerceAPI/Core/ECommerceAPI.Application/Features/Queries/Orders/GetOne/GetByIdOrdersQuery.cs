using ECommerceAPI.Application.Dtos.Orders;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOne
{
    public record GetByIdOrdersQuery(Guid OrderId) : IRequest<OrderSummaryDto>;
}
