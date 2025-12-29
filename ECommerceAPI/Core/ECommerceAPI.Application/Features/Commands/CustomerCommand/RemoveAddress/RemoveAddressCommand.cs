using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.RemoveAddress
{
    public record RemoveAddressCommand(CustomerAddressId CustomerAddressId) : IRequest;
}
