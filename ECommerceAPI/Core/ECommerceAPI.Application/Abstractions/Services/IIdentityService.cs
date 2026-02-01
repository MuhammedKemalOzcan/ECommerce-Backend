using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Dtos.UserDto;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IIdentityService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto model);
        Task<AuthResultDto> LoginAsync(LoginDto model);

        Task<TokenDto> RefreshTokenLoginAsync(string userId);
    }
}
