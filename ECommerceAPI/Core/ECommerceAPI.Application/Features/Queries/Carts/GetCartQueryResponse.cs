using ECommerceAPI.Application.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Carts
{
    public class GetCartQueryResponse
    {
        public CartDto? Data { get; set; }
        public string Message { get; set; }

    }
}
