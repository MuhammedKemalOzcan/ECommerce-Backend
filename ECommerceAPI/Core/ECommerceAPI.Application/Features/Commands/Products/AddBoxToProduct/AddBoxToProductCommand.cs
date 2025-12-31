using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.AddBoxToProduct
{
    public record AddBoxToProductCommand(Guid ProductId, string Name, int Quantity) : IRequest<Guid>;
}
