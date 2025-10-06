using ECommerceAPI.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.JwtGenerator
{
    public class JwtGenerator : ITokenHandler
    {
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(string userId, string email, IEnumerable<string> roles)
        {
            //appsettings'den JJwt config okuma.
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = _config["Jwt:Key"];
            var expires = int.Parse(_config["Jwt:Expires"] ?? "60");

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
            var token = new JwtSecurityToken(
                issuer:issuer,
                audience:audience,
                claims:claims,
                notBefore:DateTime.UtcNow,
                expires:DateTime.UtcNow.AddMinutes(expires),
                signingCredentials:signingCredentials
                );

            //Token'ı stringe çeviriyor.
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
