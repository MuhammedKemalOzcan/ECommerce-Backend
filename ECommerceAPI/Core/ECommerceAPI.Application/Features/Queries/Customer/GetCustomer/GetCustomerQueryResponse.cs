using ECommerceAPI.Application.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Customer.GetCustomer
{
    public class GetCustomerQueryResponse
    {
        public CustomerDto? Data { get; set; }
    }
}
