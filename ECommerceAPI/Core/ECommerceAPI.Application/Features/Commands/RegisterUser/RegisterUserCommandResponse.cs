using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandResponse
    {
        public bool Succeed { get; set; }
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}
