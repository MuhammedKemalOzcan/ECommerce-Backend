using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;

        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct, bool asNoTracking = true)
        {
            var query = Table.AsQueryable();
            if (asNoTracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate,ct);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(IEnumerable<Expression<Func<T, object>>>? includes = null, Expression<Func<T, bool>>? predicate = null, bool asNoTracking = true, CancellationToken ct = default)
        {
            IQueryable<T> query = Table.AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            if (predicate != null) query = query.Where(predicate);

            return await query.ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true)
        {
            var query = Table.AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
