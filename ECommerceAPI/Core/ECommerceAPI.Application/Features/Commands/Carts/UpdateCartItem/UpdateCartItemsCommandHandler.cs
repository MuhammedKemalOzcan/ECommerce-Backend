using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Application.Repositories.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCart
{
    public class UpdateCartItemsCommandHandler : IRequestHandler<UpdateCartItemsCommandRequest, UpdateCartItemsCommandResponse>
    {
        private readonly ICartItemReadRepository _cartItemRepo;
        private readonly ICartItemWriteRepository _cartItemWriteRepo;
        private readonly ICartService _cartService;
        private readonly IProductReadRepository _productReadRepo;

        public UpdateCartItemsCommandHandler(ICartItemReadRepository cartItemRepo, ICartService cartService, IProductReadRepository productReadRepo)
        {
            _cartItemRepo = cartItemRepo;
            _cartService = cartService;
            _productReadRepo = productReadRepo;
        }

        public async Task<UpdateCartItemsCommandResponse> Handle(UpdateCartItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepo.GetByIdAsync(request.CartItemId);

            if (cartItem == null) return new UpdateCartItemsCommandResponse() { Message = "Ürün bulunamadı." };

            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            if (cart == null || cart.Id != cartItem.CartId) return new UpdateCartItemsCommandResponse() { Message = "Sepetiniz bulunamadı." };

            var isOwner = await _cartService.ValidateCartOwnershipAsync(request.UserId, cart, request.SessionId, cancellationToken);
            if(!isOwner) return new UpdateCartItemsCommandResponse() { Message = "Bu sepete erişim hakkınız bulunmamaktadır." };

            var product = await _productReadRepo.GetByIdAsync(cartItem.ProductId);
            if(product == null) return new UpdateCartItemsCommandResponse() { Message = "Bu ürün bulunamadı." };

            if(product.Stock < request.Quantity) return new UpdateCartItemsCommandResponse() { Message = "Yeterli stok yok." };

            cartItem.Quantity = request.Quantity;

            await _cartItemWriteRepo.SaveChangesAsync();

            var updatedCart = _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalItemCount = cart.TotalItemCount,
                TotalAmount = cart.TotalAmount,
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

            return new UpdateCartItemsCommandResponse()
            {
                CartDto = cartDto
            };



        }
    }
}
