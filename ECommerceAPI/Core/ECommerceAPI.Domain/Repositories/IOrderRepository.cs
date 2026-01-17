using ECommerceAPI.Domain.Entities.Orders;

namespace ECommerceAPI.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync();

        void Add(Order order);
    }
}
