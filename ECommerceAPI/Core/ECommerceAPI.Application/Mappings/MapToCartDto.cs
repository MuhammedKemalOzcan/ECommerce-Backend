using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Domain.Entities.Cart;

namespace ECommerceAPI.Application.Mappings
{
    public static class CartMapper
    {
        public static CartDto ToDto(this Cart cart)
        {
            if (cart == null) return null; // veya new CartDto()

            return new CartDto
            {
                Id = cart.Id.Value,
                UserId = cart.UserId,
                SessionId = cart.SessionId,
                TotalAmount = cart.TotalAmount,
                TotalItemCount = cart.TotalItemCount,
                CartItems = cart.CartItems.Select(x => x.ToDto()).ToList()
            };
        }

        public static CartItemDto ToDto(this CartItem item)
        {
            return new CartItemDto
            {
                Id = item.Id.Value,
                ProductId = item.ProductId.Value,
                ProductName = item.ProductName,
                ProductImageUrl = item.ProductImageUrl,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            };
        }
    }
}