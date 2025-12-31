//using ECommerceAPI.Application.Repositories.Products;
//using ECommerceAPI.Domain.Entities;
//using ECommerceAPI.Persistence.Contexts;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace ECommerceAPI.Persistence.Repositories.Products
//{
//    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
//    {
//        public ProductReadRepository(ECommerceAPIDbContext context) : base(context)
//        {
//        }

//        public async Task<Product?> GetByIdAsync(Guid id, IEnumerable<Expression<Func<Product, object>>>? includes = null, bool asNoTracking = true)
//        {
//            var query = Table.AsQueryable();

//            if(asNoTracking) query = query.AsNoTracking();

//            if(includes != null)
//                foreach (var include in includes)
//                {
//                    query = query.Include(include);
//                }

//            return await query.FirstOrDefaultAsync(p => p.Id == id);

//        }
//    }
//}
