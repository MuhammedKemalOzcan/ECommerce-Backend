using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Dtos.Products
{
    public class UploadImageDto
    {
        public IFormFile File { get; set; }
        public bool IsPrimary { get; set; }
    }
}
