using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order> GetByIdAsync(OrderId orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }

        public async Task<Order> GetByTokenAsync(string paymentToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(
                o => o.PaymentToken == paymentToken);
            return order;
        }
    }
}
