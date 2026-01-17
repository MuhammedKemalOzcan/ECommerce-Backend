using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Application.Exceptions
{
    public class NotAuthenticatedException : BaseException
    {
        public NotAuthenticatedException(string message, int statusCode, string errorCode) : base(message, statusCode, errorCode)
        {
        }
    }
}
