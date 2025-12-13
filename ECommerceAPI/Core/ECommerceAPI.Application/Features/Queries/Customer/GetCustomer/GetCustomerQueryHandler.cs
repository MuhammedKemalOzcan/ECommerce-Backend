using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Customer.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQueryRequest, GetCustomerQueryResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCustomerQueryHandler(ICustomerReadRepository customerReadRepository, IHttpContextAccessor httpContextAccessor)
        {
            _customerReadRepository = customerReadRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetCustomerQueryResponse> Handle(GetCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var id = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id == null) throw new Exception("User Id bulunamadı");

            var userId = Guid.Parse(id);

            var customer = await _customerReadRepository.GetAllAsync(null, predicate: c => c.AppUserId == userId, true, ct: default);

            var customerDto = customer.Select(c => new CustomerDto
            {
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
            }).FirstOrDefault();

            return new GetCustomerQueryResponse()
            {
                Data = customerDto
            };
        }
    }
}
