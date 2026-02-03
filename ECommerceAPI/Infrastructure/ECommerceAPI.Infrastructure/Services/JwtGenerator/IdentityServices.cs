using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Dtos.UserDto;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Infrastructure.Services.JwtGenerator
{
    public class IdentityServices : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenHandler _jwtToken;
        private readonly IUnitOfWork _uow;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly ITokenHandler _tokenHandler;

        public IdentityServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenHandler jwtToken, IUnitOfWork uow, IRefreshTokenRepository refreshTokenRepo, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtToken = jwtToken;
            _uow = uow;
            _refreshTokenRepo = refreshTokenRepo;
            _tokenHandler = tokenHandler;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResultDto { Succeed = false, Error = "Email veya şifre hatalı" };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (!result.Succeeded)
            {
                return new AuthResultDto { Succeed = false, Error = "Email veya şifre hatalı" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtToken.GenerateToken(user.Id, user.Email, roles);

            var isAdmin = roles.Contains("Admin");

            //Eski tokenları silme işlemi.
            _refreshTokenRepo.RemoveRefreshTokensByUserId(user.Id);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
                Token = token.RefreshToken
            };

            _refreshTokenRepo.Add(refreshToken);
            await _uow.SaveChangesAsync(cancellationToken: default);

            var AuthResult = new AuthResultDto
            {
                AccessToken = token.AccessToken,
                RefreshToken = refreshToken.Token,
                IsAdmin = isAdmin,
                Succeed = true
            };

            return AuthResult;
        }

        public async Task<TokenDto> RefreshTokenLoginAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new UnauthorizedException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenHandler.GenerateToken(user.Id, user.Email, roles);

            _refreshTokenRepo.RemoveRefreshTokensByUserId(user.Id);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
                Token = token.RefreshToken
            };

            _refreshTokenRepo.Add(refreshToken);
            await _uow.SaveChangesAsync();

            return new TokenDto
            {
                AccessToken = token.AccessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterDto model)
        {
            var existing = await _userManager.FindByEmailAsync(model.Email);
            if (existing != null)
            {
                return new() { Succeed = false, Error = "Bu email zaten kullanılmış" };
            }

            //gelen requestlere göre yeni bir user oluştururuz.
            IdentityUser user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Email.Split("@")[0],
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                var error = string.Join(";", result.Errors.Select(e => e.Description));
                return new AuthResultDto { Succeed = false, Error = error };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtToken.GenerateToken(user.Id, user.Email, roles);

            return new AuthResultDto
            {
                Succeed = true,
                AccessToken = token.AccessToken,
                UserId = user.Id
            };
        }
    }
}