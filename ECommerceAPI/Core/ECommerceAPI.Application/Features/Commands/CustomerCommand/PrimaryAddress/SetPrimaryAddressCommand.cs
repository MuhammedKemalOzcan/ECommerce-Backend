using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.PrimaryAddress
{
    public record SetPrimaryAddressCommand(CustomerAddressId AddressId) : IRequest;
}
