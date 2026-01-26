using ECommerceAPI.Application.Dtos.Cart;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandRequest : IRequest<CartDto>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
