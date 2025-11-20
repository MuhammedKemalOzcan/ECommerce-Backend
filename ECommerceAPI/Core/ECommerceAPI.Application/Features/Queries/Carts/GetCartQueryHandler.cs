using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Carts
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQueryRequest, GetCartQueryResponse>
    {
        private readonly ICartService _cartService;

        public GetCartQueryHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<GetCartQueryResponse> Handle(GetCartQueryRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);

            if (cart == null)
            {
                var emptyCart = new CartDto
                {
                    Id = Guid.Empty,
                    CartItems = new List<CartItemDto>(),
                    UserId = request.UserId,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = null,
                    TotalAmount = 0,
                    TotalItemCount = 0,
                };

                return new GetCartQueryResponse()
                {
                    Data = emptyCart,
                    Message = "Sepet Boş."
                };
            }

            var cartDto = new CartDto
            {
                Id = cart.Id,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = null,
                TotalAmount = cart.TotalAmount,
                TotalItemCount = cart.TotalItemCount,
                UserId = request.UserId,
                CartItems = cart.CartItems.Select(ci =>
                {
                    var primary = ci.Product?.ProductGalleries?
                        .FirstOrDefault(g => g.IsPrimary)
                        ?? ci.Product?.ProductGalleries?.FirstOrDefault();

                    var imageUrl = primary?.Path;

                    return new CartItemDto
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        ProductName = ci.Product?.Name ?? string.Empty,
                        ProductImageUrl = imageUrl,
                        Quantity = ci.Quantity,
                        Stock = ci.Product?.Stock,
                        TotalPrice = ci.TotalPrice,
                        UnitPrice = ci.UnitPrice
                    };
                }).ToList()
            };

            return new GetCartQueryResponse()
            {
                Data = cartDto,
                Message = ""
            };
        }


    }
}




