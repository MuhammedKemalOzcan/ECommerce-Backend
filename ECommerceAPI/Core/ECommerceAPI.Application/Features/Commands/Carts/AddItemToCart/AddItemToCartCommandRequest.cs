using ECommerceAPI.Domain.Entities.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandRequest : IRequest<AddItemToCartCommandResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public ProductId ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
