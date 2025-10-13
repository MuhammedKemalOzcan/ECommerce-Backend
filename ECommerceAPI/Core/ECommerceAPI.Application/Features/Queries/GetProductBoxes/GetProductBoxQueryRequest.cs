using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.GetProductBoxes
{
    public class GetProductBoxQueryRequest : IRequest<GetProductBoxQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
