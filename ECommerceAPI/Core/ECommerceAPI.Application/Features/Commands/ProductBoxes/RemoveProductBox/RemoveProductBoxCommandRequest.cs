using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.RemoveProductBox
{
    public class RemoveProductBoxCommandRequest : IRequest<RemoveProductBoxCommandResponse>
    {
        public Guid BoxId { get; set; }
    }
}
