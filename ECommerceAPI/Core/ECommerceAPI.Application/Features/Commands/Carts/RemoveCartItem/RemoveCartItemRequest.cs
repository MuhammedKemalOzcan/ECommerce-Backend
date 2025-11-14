using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Carts.RemoveCartItem
{
    public class RemoveCartItemRequest : IRequest<RemoveCartItemResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public Guid CartItemId { get; set; }
    }
}
