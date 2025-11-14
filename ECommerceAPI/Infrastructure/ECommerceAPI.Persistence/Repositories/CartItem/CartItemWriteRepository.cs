using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.CartItem
{
    public class CartItemWriteRepository : WriteRepository<Domain.Entities.CartItem>, ICartItemWriteRepository
    {
        public CartItemWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
