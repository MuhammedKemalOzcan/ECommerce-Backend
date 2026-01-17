using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Orders
{
    public class OrderItem
    {
        private OrderItem() { }
        internal OrderItem(OrderId orderId, ProductId productId, string productName, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(productName)) throw new DomainException("Product name cannot be null");
            if (price < 0) throw new DomainException("Ürün fiyatı negatif olamaz.");
            if (quantity <= 0) throw new DomainException("Ürün sayısı 0'dan küçük olamaz.");
            Id = new OrderItemId(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public OrderItemId Id { get; private set; }
        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public Product Product { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

    }
}
