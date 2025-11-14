using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandRequest : IRequest<AddItemToCartCommandResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
