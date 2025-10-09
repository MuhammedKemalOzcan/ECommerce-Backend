using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class ProductGallery : BaseEntity
    {
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        //Kapak Fotoğrafı
        public bool? IsPrimary { get; set; } = false;
        public Product Product { get; set; }
    }
}
