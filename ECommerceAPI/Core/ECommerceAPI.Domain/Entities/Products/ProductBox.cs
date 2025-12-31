using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Products
{
    public class ProductBox
    {
        private ProductBox() { }

        internal ProductBox(BoxId boxId, string name, int quantity, ProductId productId)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Box item name cannot be null");
            if (quantity < 0) throw new DomainException("Quantity cannot be lower than 1");
            Id = boxId;
            Name = name;
            Quantity = quantity;
            ProductId = productId;
        }
        public BoxId Id { get; set; }

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public ProductId ProductId { get; private set; }

        internal void Update(string name, int quantity)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Box name cannot be empty");
            if (quantity < 0) throw new DomainException("Quantity cannot be negative");

            Name = name;
            Quantity = quantity;
        }

    }
}

public record BoxId(Guid Value);
