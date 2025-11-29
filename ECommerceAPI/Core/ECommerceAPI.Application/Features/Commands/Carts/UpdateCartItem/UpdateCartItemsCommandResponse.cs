using ECommerceAPI.Application.Dtos.Cart;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCart
{
    public class UpdateCartItemsCommandResponse
    {
        public CartDto Data { get; set; }
        public string Message { get; set; }
    }
}
