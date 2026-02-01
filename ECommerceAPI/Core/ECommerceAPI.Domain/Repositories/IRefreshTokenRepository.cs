using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshToken(string refreshToken);
        void Add(RefreshToken token);
        void RemoveRefreshTokensByUserId(string userId);


    }
}
