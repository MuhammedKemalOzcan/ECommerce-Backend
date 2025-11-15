using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories.Carts
{
    public class CartReadRepository : ReadRepository<Domain.Entities.Cart>, ICartsReadRepository
    {
        private readonly ECommerceAPIDbContext _context;
        public CartReadRepository(ECommerceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Domain.Entities.Cart?> GetActiveCartWithDetailsAsync(Expression<Func<Domain.Entities.Cart, bool>> predicate, CancellationToken ct = default)
        {
            return _context.Carts.Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(predicate, ct);
        }
    }
}
