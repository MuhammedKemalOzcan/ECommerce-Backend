using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class ProductGallery : File
    {
        public bool IsPrimary { get; set; } = false;
        public ICollection<Product> Product { get; set; }
    }
}
