using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public Dictionary<string, string[]>? ValidationErrors { get; set; }
    }
}
