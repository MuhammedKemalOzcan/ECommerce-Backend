using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceAPIDbContext _context;

        public OrderRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public Task<Order> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
