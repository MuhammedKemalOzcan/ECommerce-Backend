using ECommerceAPI.Application.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Carts.MergeCarts
{
    public class MergeCartsCommandResponse
    {
        public CartDto CartDto { get; set; }
        public string Message { get; set; }
    }
}
