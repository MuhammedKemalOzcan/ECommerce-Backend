using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.MergeCarts
{
    public class MergeCartsCommandHandler : IRequestHandler<MergeCartsCommandRequest, MergeCartsCommandResponse>
    {
        private readonly ICartService _cartService;
        private readonly ICartsWriteRepository _cartsWriteRepository;

        public MergeCartsCommandHandler(ICartService cartService, ICartsWriteRepository cartsWriteRepository)
        {
            _cartService = cartService;
            _cartsWriteRepository = cartsWriteRepository;
        }

        public async Task<MergeCartsCommandResponse> Handle(MergeCartsCommandRequest request, CancellationToken cancellationToken)
        {
            var guestCart = await _cartService.GetActiveCartAsync(null, request.SessionId, cancellationToken);
            if (guestCart == null)
            {
                var emptyCart = await _cartService.GetActiveCartAsync(request.UserId, null, cancellationToken);
                if (emptyCart != null)
                {
                    var emptyCartDto = new CartDto
                    {
                        Id = emptyCart.Id,
                        UserId = emptyCart.UserId,
                        TotalItemCount = emptyCart.TotalItemCount,
                        TotalAmount = emptyCart.TotalAmount,
                        CartItems = emptyCart.CartItems.Select(ci =>
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
                        }).ToList() ?? new List<CartItemDto>()

                    };
                }

                var newEmptyCart = new CartDto
                {
                    Id = Guid.Empty,
                    UserId = request.UserId,
                    TotalItemCount = 0,
                    TotalAmount = 0,
                    CartItems = new List<CartItemDto>()
                };

                return new MergeCartsCommandResponse() { Message = "Misafir Sepeti Boş" };
            }

            var userCart = await _cartService.GetActiveCartAsync(request.UserId, null, cancellationToken);
            Cart resultCart;

            if (userCart == null)
            {
                guestCart.UserId = request.UserId;
                guestCart.SessionId = null;
                guestCart.ExpiryDate = null;

                resultCart = guestCart;
            }
            else
            {
                await _cartService.MergeCartsAsync(guestCart, userCart, cancellationToken);
                resultCart = userCart;
            }

            await _cartsWriteRepository.SaveChangesAsync();
            // 4. Return merged cart
            var finalCart = await _cartService.GetActiveCartAsync(request.UserId, null, cancellationToken);

            var cartDto = new CartDto
            {
                Id = finalCart.Id,
                UserId = finalCart.UserId,
                TotalItemCount = finalCart.TotalItemCount,
                TotalAmount = finalCart.TotalAmount,
                CartItems = finalCart.CartItems.Select(ci =>
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
                }).ToList() ?? new List<CartItemDto>()
            };
            return new MergeCartsCommandResponse() { CartDto = cartDto };
        }

    }
}

