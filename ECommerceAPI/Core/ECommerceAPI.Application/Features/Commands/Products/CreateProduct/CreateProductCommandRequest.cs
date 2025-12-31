using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public record CreateProductCommandRequest(string Name, int Stock, decimal Price, string Category, string Description, string Features) : IRequest<Guid>;

}
