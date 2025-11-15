using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Enums;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Services
{
    public class CartService : ICartService
    {
        private readonly ICartsReadRepository _cartsReadRepository;
        private readonly ICartsWriteRepository _cartsWriteRepository;
        private readonly ICartItemReadRepository _itemReadRepository;

        public CartService(ICartsReadRepository cartsReadRepository, ICartsWriteRepository cartsWriteRepository, ICartItemReadRepository itemReadRepository)
        {
            _cartsReadRepository = cartsReadRepository;
            _cartsWriteRepository = cartsWriteRepository;
            _itemReadRepository = itemReadRepository;
        }

        public async Task<Cart?> GetActiveCartAsync(Guid? userId, string sessionId, CancellationToken ct = default)
        {
            Expression<Func<Cart, bool>> predicate;
            if (userId.HasValue)
            {
                predicate = x => x.UserId == userId && x.Status == CartStatus.Active;
            }
            else if (!string.IsNullOrEmpty(sessionId))
            {
                predicate = x => x.SessionId == sessionId && x.Status == CartStatus.Active;
            }
            else
            {
                return null;
            }



            var carts = await _cartsReadRepository.GetActiveCartWithDetailsAsync(predicate, ct);

            return carts;


        }
        public async Task<Cart?> GetOrCreateCartAsync(Guid? userId, string sessionId, CancellationToken ct = default)
        {
            var existingCart = await GetActiveCartAsync(userId, sessionId, ct);

            if (existingCart != null) return existingCart;

            var newCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SessionId = sessionId,
                Status = CartStatus.Active,
                CartItems = new List<CartItem>()
            };

            await _cartsWriteRepository.AddAsync(newCart);

            return newCart;

        }

        public async Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId, CancellationToken ct = default)
        {
            Expression<Func<CartItem, bool>> predicate = x => x.CartId == cartId && x.ProductId == productId;

            return await _itemReadRepository.FirstOrDefaultAsync(predicate, ct, false);
        }
        public Task<bool> ValidateCartOwnershipAsync(Guid? userId, Cart cart, string? sessionId, CancellationToken ct = default)
        {
            if (userId.HasValue && cart.UserId == userId)
            {
                return Task.FromResult(true);
            }
            if (!string.IsNullOrEmpty(sessionId) && cart.SessionId == sessionId)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<List<Cart>> GetExpiredGuestCartsAsync(CancellationToken ct = default)
        {
            Expression<Func<Cart, bool>> predicate = x =>
            x.Status == CartStatus.Active &&
            x.ExpiryDate.HasValue &&
            x.ExpiryDate.Value < DateTime.UtcNow;

            var expiredCarts = await _cartsReadRepository.GetAllAsync(null, predicate, false, ct);

            return expiredCarts.ToList();
        }


        public async Task MergeCartsAsync(Cart guestCart, Cart userCart, CancellationToken ct = default)
        {
            foreach (var guestItem in guestCart.CartItems.ToList())
            {
                var existingItem = userCart.CartItems
                    .FirstOrDefault(x => x.ProductId == guestItem.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += guestItem.Quantity;
                }
                else
                {
                    guestItem.CartId = userCart.Id;
                    userCart.CartItems.Add(guestItem);
                }
            }

            guestCart.Status = CartStatus.Merged;
        }

    }
}
