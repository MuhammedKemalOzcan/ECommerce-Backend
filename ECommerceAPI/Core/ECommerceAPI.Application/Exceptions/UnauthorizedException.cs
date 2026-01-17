using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Application.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Unauthorized") : base(message, 401, "UNAUTHORIZED")
        {
        }
    }
}
