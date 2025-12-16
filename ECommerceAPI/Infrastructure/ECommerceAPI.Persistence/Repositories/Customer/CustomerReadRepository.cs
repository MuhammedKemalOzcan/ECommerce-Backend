using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.Customer> GetByUserIdAsync(IEnumerable<Expression<Func<Domain.Entities.Customer, object>>>? includes, Guid userId, bool AsNoTracking = true, CancellationToken ct = default)
        {
            var query = Table.AsQueryable();
            if (AsNoTracking) query = query.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(c => c.AppUserId == userId, ct);
        }


    }
}
