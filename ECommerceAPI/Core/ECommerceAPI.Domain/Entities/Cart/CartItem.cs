using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Cart
{
    public class CartItem
    {
        private CartItem() { }

        internal CartItem(CartId cartId, ProductId productId, int quantity, decimal unitPrice, string productName, string productImageUrl)
        {
            if (quantity <= 0) throw new DomainException("Quantity cannot 0 or lower");
            if (unitPrice < 0) throw new DomainException("Price cannot be lower than 0");
            if (string.IsNullOrEmpty(productName))
                throw new DomainException("ProductName cannot be null");
            if (string.IsNullOrEmpty(productImageUrl))
                throw new DomainException("Image cannot be null");

            Id = new CartItemId(Guid.NewGuid());
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductName = productName;
            ProductImageUrl = productImageUrl;
        }
        public CartItemId Id { get; private set; }
        public CartId CartId { get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImageUrl { get; private set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public void UpdateCartItemQuantity(int newQuantity)
        {
            if (newQuantity < 0) throw new DomainException("Quantity cannot be 0 or lower");
            Quantity = newQuantity;
        }
        public void IncreaseQuantity(int quantity)
        {
            if (quantity < 0) throw new DomainException("Quantity cannot be 0 or lower");
            Quantity += quantity;
        }

    }
}
