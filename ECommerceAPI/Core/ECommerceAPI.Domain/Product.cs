using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public ICollection<ProductBox>? ProductBoxex { get; set; } = new List<ProductBox>();
        public ICollection<ProductGallery>? Productİmages { get; set; } = new List<ProductGallery>();

    }
}
