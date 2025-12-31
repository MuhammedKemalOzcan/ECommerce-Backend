using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Products;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface ICartService
    {
        Task<Cart?> GetActiveCartAsync(Guid? userId, string sessionId , CancellationToken ct = default);
        Task<Cart?> GetOrCreateCartAsync(Guid? userId, string sessionId, CancellationToken ct = default);
        Task<CartItem?> GetCartItemAsync(Guid cartId, ProductId productId, CancellationToken ct = default);
        Task<bool> ValidateCartOwnershipAsync(Guid? userId, Cart cart, string? sessionId, CancellationToken ct = default);

        Task<List<Cart>> GetExpiredGuestCartsAsync(CancellationToken ct = default);
        Task MergeCartsAsync(Cart guestCart, Cart userCart, CancellationToken ct = default);

    }
}
