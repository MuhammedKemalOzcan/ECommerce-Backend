using ECommerceAPI.Domain.Entities.Products;

namespace ECommerceAPI.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }
        public ProductId ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

    }
}
