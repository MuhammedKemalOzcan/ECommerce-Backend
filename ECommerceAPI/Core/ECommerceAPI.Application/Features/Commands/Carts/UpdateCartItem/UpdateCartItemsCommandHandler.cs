using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCart
{
    public class UpdateCartItemsCommandHandler : IRequestHandler<UpdateCartItemsCommandRequest, UpdateCartItemsCommandResponse>
    {
        private readonly ICartItemReadRepository _cartItemRepo;
        private readonly ICartItemWriteRepository _cartItemWriteRepo;
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateCartItemsCommandHandler> _logger;

        public UpdateCartItemsCommandHandler(ICartItemReadRepository cartItemRepo, ICartItemWriteRepository cartItemWriteRepo, ICartService cartService, IProductRepository productRepository, ILogger<UpdateCartItemsCommandHandler> logger)
        {
            _cartItemRepo = cartItemRepo;
            _cartItemWriteRepo = cartItemWriteRepo;
            _cartService = cartService;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<UpdateCartItemsCommandResponse> Handle(UpdateCartItemsCommandRequest request, CancellationToken cancellationToken)
        {


            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            if (cart == null)
            {
                _logger.LogWarning("Sepet Bulunamadı. UserId: {UserId}, SessionId: {SessionId}", request.UserId, request.SessionId);
                throw new NotFoundException("Sepet Bulunamadı");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == request.CartItemId);
            if (cartItem == null)
            {
                _logger.LogWarning("Ürün Sepette Bulunamadı. CartItemId: {CartItemId}", request.CartItemId);
                throw new NotFoundException("Ürün Bulunamadı");
            }
                

            var isOwner = await _cartService.ValidateCartOwnershipAsync(request.UserId, cart, request.SessionId, cancellationToken);
            if (!isOwner) throw new UnauthorizedAccessException();

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Ürün Bulunamadı. ProductId: {ProductId}", cartItem.ProductId);
                throw new NotFoundException("Ürün Bulunamadı");
            }

            if (product.Stock < request.Quantity) throw new StockException();

            cartItem.Quantity = request.Quantity;

            _cartItemWriteRepo.Update(cartItem);
            await _cartItemWriteRepo.SaveChangesAsync();

            var updatedCart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            var cartDto = new CartDto
            {
                Id = updatedCart.Id,
                UserId = updatedCart.UserId,
                TotalItemCount = updatedCart.TotalItemCount,
                TotalAmount = updatedCart.TotalAmount,
                CartItems = updatedCart.CartItems.Select(ci =>
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

            return new UpdateCartItemsCommandResponse()
            {
                Data = cartDto
            };



        }
    }
}
