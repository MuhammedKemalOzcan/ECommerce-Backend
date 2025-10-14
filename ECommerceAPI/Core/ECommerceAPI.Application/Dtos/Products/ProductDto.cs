namespace ECommerceAPI.Application.Dtos.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public List<ProductBoxDto>? ProductBoxes { get; set; } = new();
        public List<ProductGalleryDto>? ProductGalleries { get; set; } = new();
    }
}
