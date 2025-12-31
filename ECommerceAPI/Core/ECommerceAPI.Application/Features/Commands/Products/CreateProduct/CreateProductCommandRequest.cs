using ECommerceAPI.Application.Dtos.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public record CreateProductCommandRequest(string Name, int Stock, decimal Price, string Category, string Description, string Features, List<ProductBoxRequestDto> ProductBoxes) : IRequest<ProductDto>;

}
public class ProductBoxRequestDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}


