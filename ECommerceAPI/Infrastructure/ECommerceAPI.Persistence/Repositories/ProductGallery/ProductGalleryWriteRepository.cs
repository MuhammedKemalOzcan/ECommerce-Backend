using ECommerceAPI.Application.Repositories.ProductGallery;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.ProductGallery
{
    public class ProductGalleryWriteRepository : WriteRepository<Domain.Entities.ProductGallery>, IProductGalleryWriteRepository
    {
        public ProductGalleryWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
