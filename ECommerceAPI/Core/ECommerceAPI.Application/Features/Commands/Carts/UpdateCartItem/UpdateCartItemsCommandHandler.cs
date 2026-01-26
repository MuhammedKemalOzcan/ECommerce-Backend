using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem;
using ECommerceAPI.Application.Mappings;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Cart;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.UpdateCart
{
    public class UpdateCartItemsCommandHandler : IRequestHandler<UpdateCartItemsCommandRequest, CartDto>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCartItemsCommandHandler> _logger;

        public UpdateCartItemsCommandHandler(IProductRepository productRepository, ICartRepository cartRepository, IUnitOfWork unitOfWork, ILogger<UpdateCartItemsCommandHandler> logger)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CartDto> Handle(UpdateCartItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository
                .GetActiveCartAsync(request.UserId, request.SessionId);
            if (cart is null)
            {
                cart = Cart.Create(request.UserId, request.SessionId);
                _cartRepository.Add(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id.Value == request.CartItemId);

            if (cartItem == null)
            {
                _logger.LogWarning("Ürün Sepette Bulunamadı. CartItemId: {CartItemId}", request.CartItemId);
                throw new NotFoundException("Ürün Bulunamadı");
            }

            cartItem.UpdateCartItemQuantity(request.Quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var cartDto = cart.ToDto();
            return cartDto;

        }
    }
}
