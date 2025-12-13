using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.UserDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.JwtGenerator
{
    public class IdentityServices : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenHandler _jwtToken;

        public IdentityServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenHandler jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtToken = jwtToken;
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

            return new AuthResultDto { Succeed = true, Token = token };

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
                Token = token,
                UserId = user.Id
            };


        }
    }
}