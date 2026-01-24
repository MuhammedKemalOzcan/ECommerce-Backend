using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.Orders;
using ECommerceAPI.Domain.Entities.Customer;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOne
{
    public class GetByIdOrdersQueryHandler : IRequestHandler<GetByIdOrdersQuery, OrderSummaryDto>
    {
        private readonly IEcommerceAPIDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ICustomerRepository _customerRepository;

        public GetByIdOrdersQueryHandler(IEcommerceAPIDbContext context, ICurrentUserService currentUser, ICustomerRepository customerRepository)
        {
            _context = context;
            _currentUser = currentUser;
            _customerRepository = customerRepository;
        }

        public async Task<OrderSummaryDto> Handle(GetByIdOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetCurrentUserId();
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null) throw new NotFoundException("Customer not found");

            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == new OrderId(request.OrderId))
                .Select(o => new OrderSummaryDto
                {
                    Id = o.Id.Value,
                    OrderDate = o.OrderDate,
                    GrandTotal = o.GrandTotal,
                    OrderCode = o.OrderCode,
                    DeliveryStatus = o.Status,
                    OrderItems = o.OrderItems.Select(i => new OrderItemsDto
                    {
                        ProductName = i.ProductName,
                        Price = i.Price,
                        Quantity = i.Quantity,
                        ImageUrl = i.Product.ProductGalleries
                        .Where(pg => pg.IsPrimary == true)
                        .Select(pg => pg.Path)
                        .FirstOrDefault()
                    }).ToList(),
                    ShippingAddress = new LocationDto
                    {
                        City = o.ShippingAddress.City,
                        Street = o.ShippingAddress.Street,
                        Country = o.ShippingAddress.Country,
                        ZipCode = o.ShippingAddress.ZipCode
                    },
                    Customer = new CustomerDto
                    {
                        Id = customer.Id.Value,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                    },
                    PaymentInfo = new PaymentInfoDto
                    {
                        CardLastFourDigits = o.PaymentInfo.CardLastFourDigits,
                        CardAssociation = o.PaymentInfo.CardAssociation,
                        CardHolderName = o.PaymentInfo.CardHolderName
                    },
                }).FirstOrDefaultAsync(cancellationToken);

            if (order == null) throw new NotFoundException($"Product with ID {request.OrderId} not found.");

            return order;
        }
    }
}
