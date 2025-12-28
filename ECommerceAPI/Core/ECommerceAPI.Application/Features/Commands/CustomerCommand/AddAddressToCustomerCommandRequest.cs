using ECommerceAPI.Application.Dtos.Customer;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand
{
    public class AddAddressToCustomerCommandRequest() : IRequest<Guid>
    {
        public string Title { get; set; }
        public LocationDto Location { get; set; }
        public bool IsPrimary { get; set; }

    }
}
