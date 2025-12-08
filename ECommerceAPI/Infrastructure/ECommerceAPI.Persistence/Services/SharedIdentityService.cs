using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ECommerceAPI.Persistence.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
              ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated or ID not found in token");

            return userId;
        }
    }
}
