using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories.Carts
{
    public class CartReadRepository : ReadRepository<Domain.Entities.Cart>, ICartsReadRepository
    {
        public CartReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
