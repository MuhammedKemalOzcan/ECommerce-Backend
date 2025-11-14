using ECommerceAPI.Application.Features.Commands.Carts.UpdateCart;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem
{
    public class UpdateCartItemsCommandRequest : IRequest<UpdateCartItemsCommandResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }

    }
}
