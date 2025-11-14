using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.ProductBoxes.CreateProductBox;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, AddItemToCartCommandResponse>
    {
        private readonly ICartService _cartService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICartItemWriteRepository _cartItemWriteRepository;

        public AddItemToCartCommandHandler(ICartService cartService, IProductReadRepository productReadRepository, ICartItemWriteRepository cartItemWriteRepository)
        {
            _cartService = cartService;
            _productReadRepository = productReadRepository;
            _cartItemWriteRepository = cartItemWriteRepository;
        }

        public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetOrCreateCartAsync(request.UserId, request.SessionId, cancellationToken);

            var product = await _productReadRepository.GetByIdAsync(request.ProductId);
            if (product == null) return new AddItemToCartCommandResponse { Message = "Ürün bulunamadı" };

            var existingItem = await _cartService.GetCartItemAsync(cart.Id, request.ProductId, cancellationToken);
            var totalQuantity = (existingItem?.Quantity ?? 0) + request.Quantity;

            if (product.Stock < totalQuantity)
            {
                return new AddItemToCartCommandResponse { Message = "Stok yeterli değil." };
            }

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price
                };
                await _cartItemWriteRepository.AddAsync(newCartItem);
            }

            await _cartItemWriteRepository.SaveChangesAsync();

            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = request.UserId,
                TotalAmount = cart.TotalAmount,
                TotalItemCount = cart.TotalItemCount,
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
                }).ToList() ?? new List<CartItemDto>()
            };

            return new AddItemToCartCommandResponse
            {
                CartDto = cartDto
            };


        }
    }
}
