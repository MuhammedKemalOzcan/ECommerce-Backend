using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveBoxFromProduct
{
    public record RemoveBoxFromProductCommand(Guid BoxId, Guid ProductId) : IRequest;
}
