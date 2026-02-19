using ECommerceAPI.Application.Dtos.Orders;
using ECommerceAPI.Application.Utilities;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetAllOrders
{
    public record GetAllOrdersQuery(int PageIndex, int PageSize,string? SearchTerm) : IRequest<PaginatedList<OrderSummaryDto>>;
}