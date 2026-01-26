using ECommerceAPI.Domain.Entities.Cart;

namespace ECommerceAPI.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetActiveCartAsync(Guid? userId, string? sessionId);
        void Add(Cart cart);

        void Remove(Cart cart);
    }
}
