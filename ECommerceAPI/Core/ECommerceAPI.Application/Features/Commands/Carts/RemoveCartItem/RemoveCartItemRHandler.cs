using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Carts.RemoveCartItem
{
    public class RemoveCartItemRHandler : IRequestHandler<RemoveCartItemRequest, RemoveCartItemResponse>
    {
        private readonly ICartService _cartService;
        private readonly ICartItemReadRepository _cartItemReadRepository;
        private readonly ICartItemWriteRepository _cartItemWriteRepository;
        private readonly ILogger<RemoveCartItemRHandler> _logger;
        public RemoveCartItemRHandler(ICartService cartService, ICartItemReadRepository cartItemReadRepository, ICartItemWriteRepository cartItemWriteRepository, ILogger<RemoveCartItemRHandler> logger)
        {
            _cartService = cartService;
            _cartItemReadRepository = cartItemReadRepository;
            _cartItemWriteRepository = cartItemWriteRepository;
            _logger = logger;
        }

        public async Task<RemoveCartItemResponse> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            
            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            if (cart == null)
            {
                _logger.LogWarning("Sepet bulunamadı. UserId: {UserId}, SessionId: {SessionId}", request.UserId, request.SessionId);
                throw new NotFoundException("Sepet Bulunamadı");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == request.CartItemId);
            if (cartItem == null)
            {
                _logger.LogWarning("Sepet ürünü bulunamadı. CartItemId: {CartItemId}", request.CartItemId);
                throw new NotFoundException("Ürün Bulunamadı");
            }

            var isOwner = await _cartService.ValidateCartOwnershipAsync(request.UserId, cart, request.SessionId, cancellationToken);
            if (!isOwner) throw new UnauthorizedAccessException("Bu sepete erişim hakkınız bulunmamaktadır.");

            _cartItemWriteRepository.Remove(cartItem);

            await _cartItemWriteRepository.SaveChangesAsync();

            return new RemoveCartItemResponse() { Message = "Silme işlemi başarılı." };


        }
    }
}
