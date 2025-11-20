using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message) : base(message, 400, "BUSINESS_ERROR")
        {
        }
    }
}
