using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using System.Threading;

namespace ECommerceAPI.Domain.Entities.Cart
{
    public class Cart
    {
        private Cart() { }
        public CartId Id { get; private set; }
        public Guid? UserId { get; private set; }
        public string? SessionId { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public CartStatus Status { get; private set; }
        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();
        public decimal TotalAmount => _cartItems.Sum(x => x.TotalPrice);
        public int TotalItemCount => _cartItems.Sum(x => x.Quantity);

        private Cart(Guid? userId, string? sessionId)
        {
            if (userId is null && string.IsNullOrEmpty(sessionId))
                throw new DomainException("Cart must belong to either a User or a Session!");

            Id = new CartId(Guid.NewGuid());
            UserId = userId;
            SessionId = sessionId;
            ExpiryDate = userId.HasValue ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddDays(2);
            Status = CartStatus.Active;
        }

        public static Cart Create(Guid? userId, string? sessionId)
        {
            return new Cart(userId, sessionId);
        }

        public void AddCartItem(ProductId productId, int quantity, decimal unitPrice, string productName, string productImageUrl)
        {
            var existingItem = _cartItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var cartItem = new CartItem(
                    Id,
                    productId,
                    quantity,
                    unitPrice,
                    productName,
                    productImageUrl
                    );
                _cartItems.Add(cartItem);

            }
        }
        public void MergeCarts(Cart guestCart)
        {
            if (guestCart == null || !guestCart.CartItems.Any()) return;
            foreach (var item in guestCart.CartItems.ToList())
            {
                this.AddCartItem(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.ProductName,
                    item.ProductImageUrl
                    );
            }
        }
        public void ClearCart()
        {
            _cartItems.Clear();
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            _cartItems.Remove(cartItem);
        }


    }
}
