using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public List<ProductBoxDto>? ProductBoxes { get; set; } = new List<ProductBoxDto>();
        public List<string>? ProductGalleries { get; set; } = new List<string>();
    }
}
