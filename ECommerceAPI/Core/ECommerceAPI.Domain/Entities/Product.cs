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
        public string? Description { get; set; }
        public string? Features { get; set; }
        public ICollection<ProductBox> ProductBoxes { get; set; } = new List<ProductBox>();
        public ICollection<ProductGallery> ProductGalleries { get; set; } = new List<ProductGallery>();

    }
}
