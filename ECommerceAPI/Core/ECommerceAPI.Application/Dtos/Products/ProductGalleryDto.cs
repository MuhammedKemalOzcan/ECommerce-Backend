using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Dtos.Products
{
    public class ProductGalleryDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }

    }
}
