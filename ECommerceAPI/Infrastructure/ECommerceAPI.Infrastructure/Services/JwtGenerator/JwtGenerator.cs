using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services.JwtGenerator
{
    public class JwtGenerator : ITokenHandler
    {
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }
        public TokenDto GenerateToken(string userId, string email, IEnumerable<string> roles)
        {
            //appsettings'den Jwt config okuma.
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = _config["Jwt:Key"];
            var expires = int.Parse(_config["Jwt:ExpiresMinutes"]);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.NameIdentifier,userId)
            };

            //rolleri tokena ekleme işlemi:
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //Token oluşturma aşaması
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();

            return new TokenDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
            };
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
