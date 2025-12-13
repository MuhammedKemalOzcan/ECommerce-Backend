using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.Customer> GetByUserIdAsync(Guid userId, bool AsNoTracking = true, CancellationToken ct = default)
        {
            var query = Table.AsQueryable();
            if (AsNoTracking) query.AsNoTracking();

            return await query.FirstOrDefaultAsync(c => c.AppUserId == userId, ct);
        }
    }
}
