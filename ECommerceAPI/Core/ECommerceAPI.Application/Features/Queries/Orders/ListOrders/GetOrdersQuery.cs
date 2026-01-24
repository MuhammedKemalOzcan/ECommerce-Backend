using ECommerceAPI.Application.Dtos.Orders;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.ListOrders
{
    public record GetOrdersQuery() : IRequest<List<OrderSummaryDto>>;
}
