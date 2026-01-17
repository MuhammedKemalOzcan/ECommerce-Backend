using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.Orders;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Queries.Orders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderSummaryDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetOrdersQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEcommerceAPIDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetOrdersQueryHandler(IProductRepository productRepository, ILogger<GetOrdersQueryHandler> logger, IUnitOfWork uow, ICustomerRepository customerRepository, IEcommerceAPIDbContext context, ICurrentUserService currentUser)
        {
            _productRepository = productRepository;
            _logger = logger;
            _uow = uow;
            _customerRepository = customerRepository;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<OrderSummaryDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetCurrentUserId();
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null) throw new NotFoundException("Customer not found");

            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customer.Id)
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
                })
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(cancellationToken);

            return order;
        }
    }
}
