using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ECommerceAPI.Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            var userIdString = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                throw new UnauthorizedException("Yetkisiz erişim!");

            return userId;
        }

        public bool IsAdmin()
        {
            var user = _contextAccessor.HttpContext?.User;

            if (user == null || user.Identity?.IsAuthenticated != true)
            {
                return false;
            }
            return user.IsInRole("Admin");
        }
    }
}



