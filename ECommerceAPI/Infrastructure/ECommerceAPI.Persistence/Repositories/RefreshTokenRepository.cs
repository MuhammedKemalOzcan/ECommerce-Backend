using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ECommerceAPIDbContext _context;

        public RefreshTokenRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public void Add(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .Where(r => r.ExpiresOnUtc > DateTime.UtcNow)
                .FirstOrDefaultAsync(r => r.Token == token);

            return refreshToken;
        }

        public void RemoveRefreshTokensByUserId(string userId)
        {
            var oldTokens = _context.RefreshTokens.Where(r => r.UserId == userId.ToString());
            _context.RemoveRange(oldTokens);
        }
    }
}
