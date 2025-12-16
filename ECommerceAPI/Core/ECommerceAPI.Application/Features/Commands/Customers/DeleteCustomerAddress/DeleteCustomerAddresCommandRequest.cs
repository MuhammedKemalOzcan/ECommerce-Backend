using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Customers.DeleteCustomerAddress
{
    public class DeleteCustomerAddresCommandRequest : IRequest<DeleteCustomerAddresCommandResponse>
    {
        public Guid addressId { get; set; }
    }
}
