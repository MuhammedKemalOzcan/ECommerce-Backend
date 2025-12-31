using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateBoxItem
{
    public record UpdateBoxItemCommand(Guid ProductId, Guid BoxId, string Name, int Quantity) : IRequest;
}
