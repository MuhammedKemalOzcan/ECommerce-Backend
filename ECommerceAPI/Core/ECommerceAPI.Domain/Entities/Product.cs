using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public List<ProductBox>? ProductBoxes { get; set; } = new List<ProductBox>();
        public List<ProductGallery>? ProductGalleries { get; set; } = new List<ProductGallery>();

    }
}
