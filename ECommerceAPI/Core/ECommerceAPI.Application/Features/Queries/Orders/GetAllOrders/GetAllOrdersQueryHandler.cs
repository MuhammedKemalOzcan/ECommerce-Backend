using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.Orders;
using ECommerceAPI.Application.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderSummaryDto>>
    {
        private readonly IEcommerceAPIDbContext _context;

        public GetAllOrdersQueryHandler(IEcommerceAPIDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<OrderSummaryDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {

            var query = from order in _context.Orders
                        join customer in _context.Customers
                        on order.CustomerId equals customer.Id
                        select new { order, customer };

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string search = request.SearchTerm.ToLower().Trim();

                query = query.Where(x => x.order.OrderCode.ToLower().Contains(search) || (x.customer.FirstName + " " + x.customer.LastName).ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);


            var orders = await query
        .OrderByDescending(x => x.order.OrderDate)
        .Select(x => new OrderSummaryDto
        {
            Id = x.order.Id.Value,
            OrderDate = x.order.OrderDate,
            GrandTotal = x.order.GrandTotal,
            OrderCode = x.order.OrderCode,
            DeliveryStatus = x.order.Status,
            ShippingCost = x.order.ShippingCost,
            SubTotal = x.order.SubTotal,
            OrderItems = x.order.OrderItems.Select(i => new OrderItemsDto
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
                City = x.order.ShippingAddress.City,
                Street = x.order.ShippingAddress.Street,
                Country = x.order.ShippingAddress.Country,
                ZipCode = x.order.ShippingAddress.ZipCode
            },
            Customer = new CustomerDto
            {
                FirstName = x.customer.FirstName + " " + x.customer.LastName,
                Email = x.customer.Email,
                PhoneNumber = x.customer.PhoneNumber,
            }
        })
        .AsNoTracking()
        .Skip((request.PageIndex - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

            return new PaginatedList<OrderSummaryDto>(orders, request.PageIndex, totalCount, request.PageSize);
        }
    }
}