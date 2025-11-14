using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface ICartService
    {
        Task<Cart?> GetActiveCartAsync(Guid? userId, string sessionId , CancellationToken ct = default);
        Task<Cart?> GetOrCreateCartAsync(Guid? userId, string sessionId, CancellationToken ct = default);
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId, CancellationToken ct = default);
        Task<bool> ValidateCartOwnershipAsync(Guid? userId, Cart cart, string? sessionId, CancellationToken ct = default);

        Task<List<Cart>> GetExpiredGuestCartsAsync(CancellationToken ct = default);
        Task MergeCartsAsync(Cart guestCart, Cart userCart, CancellationToken ct = default);


    }
}
