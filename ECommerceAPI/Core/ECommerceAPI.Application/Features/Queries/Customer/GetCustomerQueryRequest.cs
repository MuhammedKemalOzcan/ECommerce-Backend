using ECommerceAPI.Application.Dtos.Customer;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Customer
{
    public record GetCustomerQueryRequest(Guid AppUserId) : IRequest<CustomerDto>;
}
