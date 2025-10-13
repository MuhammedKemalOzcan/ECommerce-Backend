using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.ProductBoxes
{
    public class ProductBoxReadRepository : ReadRepository<ProductBox>, IProductBoxReadRepository
    {
        public ProductBoxReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
