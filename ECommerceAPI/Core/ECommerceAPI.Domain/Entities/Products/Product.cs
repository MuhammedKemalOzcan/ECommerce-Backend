using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Products
{
    public class Product
    {
        private Product() { }
        public ProductId Id { get; set; }
        public string Name { get; private set; }
        public int Stock { get; private set; }
        public decimal Price { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string Features { get; private set; }

        private readonly List<ProductBox> _productBoxes = new();
        public IReadOnlyList<ProductBox> ProductBoxes => _productBoxes.AsReadOnly();

        private readonly List<ProductGallery> _productGalleries = new();
        public IReadOnlyList<ProductGallery> ProductGalleries => _productGalleries.AsReadOnly();

        public static Product Create(string name, int stock, decimal price, string category, string description, string features)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Product name cannot be null");
            if (string.IsNullOrEmpty(category)) throw new DomainException("Category cannot be null");
            if (string.IsNullOrEmpty(description)) throw new DomainException("Description cannot be null");
            if (string.IsNullOrEmpty(features)) throw new DomainException("Features cannot be null");
            if (stock < 0) throw new DomainException("Stock cannot be lower than 1");
            if (price < 1) throw new DomainException("Price cannot be negative");

            var product = new Product
            {
                Id = new ProductId(Guid.NewGuid()),
                Name = name,
                Stock = stock,
                Price = price,
                Category = category,
                Description = description,
                Features = features
            };
            return product;
        }

        public void Update(string name, int stock, decimal price, string category, string description, string features)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Name cannot be empty");
            if (stock < 0) throw new DomainException("Stock cannot be negative");
            if (price < 0) throw new DomainException("Price cannot be negative");

            Name = name;
            Stock = stock;
            Price = price;
            Category = category;
            Description = description;
            Features = features;
        }

        public void AddBox(BoxId boxId, string name, int quantity)
        {
            var box = new ProductBox(boxId, name, quantity, Id);
            _productBoxes.Add(box);
        }

        public void RemoveBox(BoxId boxId)
        {
            var box = _productBoxes.FirstOrDefault(b => b.Id == boxId);
            if (box is null) throw new DomainException("Product box cannot be found");

            _productBoxes.Remove(box);
        }


    }


}
