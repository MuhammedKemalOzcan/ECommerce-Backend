using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

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
            var includes = new Expression<Func<Domain.Entities.Customer, object>>[]
            {
                c => c.Addresses
            };

            var id = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id == null) throw new Exception("User Id bulunamadı");

            var userId = Guid.Parse(id);

            var customer = await _customerReadRepository.GetByUserIdAsync(includes, userId, true, cancellationToken);

            var customerDto = new CustomerDto
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Addresses = customer.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    Country = a.Country,
                    ZipCode = a.ZipCode,
                }).ToList()
            };

            return new GetCustomerQueryResponse()
            {
                Data = customerDto
            };
        }
    }
}
