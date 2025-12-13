using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Dtos.UserDto
{
    public class AuthResultDto
    {
        public string UserId { get; set; }
        public bool Succeed { get; set; }
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}
