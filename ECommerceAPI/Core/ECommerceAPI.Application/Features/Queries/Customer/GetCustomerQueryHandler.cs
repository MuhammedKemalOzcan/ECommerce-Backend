using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Customer
{
    internal sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQueryRequest, CustomerDto>
    {
        private readonly IEcommerceAPIDbContext _context;

        public GetCustomerQueryHandler(IEcommerceAPIDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(GetCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Customers
                .AsNoTracking()
                .Where(c => c.AppUserId == request.AppUserId)
                .Select(c => new CustomerDto
                {
                    Id = c.Id.Value,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Addresses = c.Addresses.Select(a => new AddressDto
                    {
                        Id = a.Id.Value,
                        Title = a.Title,
                        IsPrimary = a.IsPrimary,
                        Location = new LocationDto
                        {
                            City = a.Location.City,
                            Street = a.Location.Street,
                            Country = a.Location.Country,
                            ZipCode = a.Location.ZipCode
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            if (response == null) throw new NotFoundException($"Customer with AppUserId {request.AppUserId} was not found.");

            return response;
        }
    }
}
