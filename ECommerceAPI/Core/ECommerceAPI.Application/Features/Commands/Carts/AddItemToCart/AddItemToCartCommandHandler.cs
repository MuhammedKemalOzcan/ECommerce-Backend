using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, AddItemToCartCommandResponse>
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemWriteRepository _cartItemWriteRepository;
        private readonly ILogger<AddItemToCartCommandHandler> _logger;

        public AddItemToCartCommandHandler(ICartService cartService, ICartItemWriteRepository cartItemWriteRepository, ILogger<AddItemToCartCommandHandler> logger, IProductRepository productRepository)
        {
            _cartService = cartService;
            _cartItemWriteRepository = cartItemWriteRepository;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetOrCreateCartAsync(request.UserId, request.SessionId, cancellationToken);

            var product = await _productRepository.GetByIdAsync(new ProductId(request
                .ProductId));
            if (product == null)
            {
                _logger.LogWarning("Ürün Bulunamadı. ProductId: {ProductId}", request.ProductId);
                throw new NotFoundException("Ürün bulunamadı");
            }

            var existingItem = await _cartService.GetCartItemAsync(cart.Id, new ProductId(request.ProductId), cancellationToken);
            var totalQuantity = (existingItem?.Quantity ?? 0) + request.Quantity;

            if (product.Stock < totalQuantity)
            {
                _logger.LogWarning(
                "Yetersiz stok. ProductId: {ProductId}, Stock: {Stock}, Requested: {RequestedQuantity}",
                product.Id, product.Stock, totalQuantity);
                throw new StockException(message: $"Stok yeterli değil stok miktarı: {product.Stock}");
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
                    ProductId = new ProductId(request.ProductId),
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
                        ProductId = ci.ProductId.Value,
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
                Data = cartDto,
                Message = "Ürün başarıyla eklendi."
            };


        }
    }
}
