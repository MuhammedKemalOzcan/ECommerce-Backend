using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Cart
{
    public class CartWriteRepository : WriteRepository<Domain.Entities.Cart>, ICartsWriteRepository
    {
        public CartWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
