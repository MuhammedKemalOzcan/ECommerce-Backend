using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Carts
{
    public class GetCartQueryRequest : IRequest<GetCartQueryResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
    }
}
