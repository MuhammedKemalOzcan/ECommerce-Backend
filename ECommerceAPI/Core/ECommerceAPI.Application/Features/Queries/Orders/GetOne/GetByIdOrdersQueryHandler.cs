using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.Orders;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOne
{
    public class GetByIdOrdersQueryHandler : IRequestHandler<GetByIdOrdersQuery, OrderSummaryDto>
    {
        private readonly IEcommerceAPIDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetByIdOrdersQueryHandler(IEcommerceAPIDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<OrderSummaryDto> Handle(GetByIdOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = from order in _context.Orders
                        join customer in _context.Customers
                        on order.CustomerId equals customer.Id
                        select new { order, customer };

            query = query.Where(x => x.order.Id == new OrderId(request.OrderId));

            bool isAdmin = _currentUser.IsAdmin();

            if (!isAdmin)
            {
                var currentUserId = _currentUser.GetCurrentUserId();

                query = query.Where(x => x.customer.AppUserId == currentUserId);
            }

            var result = await query.Select(x => new OrderSummaryDto
            {
                Id = x.order.Id.Value,
                OrderDate = x.order.OrderDate,
                GrandTotal = x.order.GrandTotal,
                OrderCode = x.order.OrderCode,
                DeliveryStatus = x.order.Status,
                ShippingCost = x.order.ShippingCost,
                SubTotal = x.order.SubTotal,
                ShippedDate = x.order.ShippedDate,
                DeliveredDate = x.order.DeliveredDate,
                Customer = new CustomerDto
                {
                    Id = x.customer.Id.Value,
                    FirstName = x.customer.FirstName,
                    LastName = x.customer.LastName,
                    Email = x.customer.Email,
                    PhoneNumber = x.customer.PhoneNumber
                },
                ShippingAddress = new LocationDto
                {
                    City = x.order.ShippingAddress.City,
                    Street = x.order.ShippingAddress.Street,
                    Country = x.order.ShippingAddress.Country,
                    ZipCode = x.order.ShippingAddress.ZipCode
                },

                PaymentInfo = new PaymentInfoDto
                {
                    CardLastFourDigits = x.order.PaymentInfo.CardLastFourDigits,
                    CardAssociation = x.order.PaymentInfo.CardAssociation,
                    CardHolderName = x.order.PaymentInfo.CardHolderName
                },
                OrderItems = x.order.OrderItems.Select(i => new OrderItemsDto
                {
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ProductGalleries
                        .Where(pg => pg.IsPrimary == true)
                        .Select(pg => pg.Path)
                        .FirstOrDefault()
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
            if (result == null)
            {
                throw new NotFoundException($"Order with ID {request.OrderId} not found.");
            }

            return result;
        }
    }
}