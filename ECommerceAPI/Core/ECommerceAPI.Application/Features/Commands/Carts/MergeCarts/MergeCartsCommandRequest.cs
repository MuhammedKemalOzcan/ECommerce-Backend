using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Carts.MergeCarts
{
    public class MergeCartsCommandRequest : IRequest<MergeCartsCommandResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        
    }
}
