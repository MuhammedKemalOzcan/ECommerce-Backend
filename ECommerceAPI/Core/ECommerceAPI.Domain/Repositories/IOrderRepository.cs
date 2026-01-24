using ECommerceAPI.Domain.Entities.Orders;

namespace ECommerceAPI.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(OrderId orderId);
        Task<Order> GetByTokenAsync(string paymentToken);

        void Add(Order order);
    }
}
