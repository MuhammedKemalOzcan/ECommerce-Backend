using ECommerceAPI.Application.Repositories.ProductGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.ProductGallery
{
    public class ProductGalleryReadRepository : ReadRepository<Domain.Entities.ProductGallery>, IProductGalleryReadRepository
    {
        public ProductGalleryReadRepository(ECommerceAPI.Persistence.Contexts.ECommerceAPIDbContext context) : base(context)
        {
        }
    
    }
}
