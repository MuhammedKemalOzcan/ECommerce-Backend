using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class NotAuthenticatedException : BaseException
    {
        public NotAuthenticatedException(string message, int statusCode, string errorCode) : base(message, statusCode, errorCode)
        {
        }
    }
}
