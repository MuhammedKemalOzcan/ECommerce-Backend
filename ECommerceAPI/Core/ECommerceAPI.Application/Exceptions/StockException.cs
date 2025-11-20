using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class StockException : BaseException
    {
        public StockException(string message) : base(message, 400, "INSUFFICIENT_STOCK")
        {
        }
    }
}
