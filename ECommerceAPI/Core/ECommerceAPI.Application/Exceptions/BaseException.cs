using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public abstract class BaseException : Exception
    {

        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected BaseException(string message, int statusCode, string errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }


    }
}
