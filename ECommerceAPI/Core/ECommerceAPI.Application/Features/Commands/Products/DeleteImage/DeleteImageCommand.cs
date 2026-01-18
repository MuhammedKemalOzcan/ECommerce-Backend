using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.DeleteImage
{
    public record DeleteImageCommand(Guid imageId, Guid ProductId) : IRequest<Guid>;
}
