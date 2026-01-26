using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Carts
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQueryRequest, CartDto>
    {
        private readonly IEcommerceAPIDbContext _context;

        public GetCartQueryHandler(IEcommerceAPIDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> Handle(GetCartQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Carts.AsNoTracking().
                Where(c => c.Status == CartStatus.Active);

            if (request.UserId != null)
                query = query.Where(c => c.UserId == request.UserId);

            if (request.UserId == null && request.SessionId != null)
                query = query.Where(c => c.SessionId == request.SessionId);

            var cart = await query
                .Include(c => c.CartItems)
                .Select(c => new CartDto
                {
                    Id = c.Id.Value,
                    UserId = c.UserId,
                    TotalAmount = c.TotalAmount,
                    TotalItemCount = c.TotalItemCount,
                    CartItems = c.CartItems.Select(x => new CartItemDto
                    {
                        Id = x.Id.Value,
                        ProductId = x.ProductId.Value,
                        ProductImageUrl = x.ProductImageUrl,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity,
                        TotalPrice = x.TotalPrice,
                        UnitPrice = x.UnitPrice
                    }).OrderByDescending(x => x.ProductName)
                    .ToList()
                }).FirstOrDefaultAsync();

            //Sepete ürün eklenmemesi durumunda boş sepet oluşturulur.
            if (cart == null)
            {
                var emptyCart = new CartDto
                {
                    Id = Guid.Empty,
                    CartItems = new List<CartItemDto>(),
                    UserId = request.UserId,
                    TotalItemCount = 0,
                    TotalAmount = 0,
                };

                return emptyCart;
            }

            return cart;
        }
    }
}




