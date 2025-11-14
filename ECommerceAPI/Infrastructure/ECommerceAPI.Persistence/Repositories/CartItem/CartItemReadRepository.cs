using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.CartItem
{
    public class CartItemReadRepository : ReadRepository<Domain.Entities.CartItem>, ICartItemReadRepository
    {
        public CartItemReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
