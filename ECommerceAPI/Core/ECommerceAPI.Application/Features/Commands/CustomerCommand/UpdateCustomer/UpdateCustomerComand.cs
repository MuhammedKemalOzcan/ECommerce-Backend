using ECommerceAPI.Application.Dtos.Customer;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.UpdateCustomer
{
    public record UpdateCustomerComand(string FirstName,string LastName,string Email,string PhoneNumber) : IRequest<CustomerDto>;
}
