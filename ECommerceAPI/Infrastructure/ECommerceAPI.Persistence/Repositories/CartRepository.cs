using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceAPIDbContext _context;

        public CartRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Entities.Cart.Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public async Task<Domain.Entities.Cart.Cart?> GetActiveCartAsync(Guid? userId, string? sessionId)
        {
            var query = _context.Carts.Where(c => c.Status == Domain.Enums.CartStatus.Active);

            if (userId != null)
                query = query.Where(c => c.UserId == userId);

            if (userId == null && sessionId != null)
                query = query.Where(c => c.SessionId == sessionId);

            var cart = await query.Include(c => c.CartItems).FirstOrDefaultAsync();

            return cart;
        }

        public void Remove(Domain.Entities.Cart.Cart cart)
        {
            _context.Carts.Remove(cart);
        }
    }
}
