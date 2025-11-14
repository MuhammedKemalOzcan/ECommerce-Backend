using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using MediatR;
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

        public RemoveCartItemRHandler(ICartService cartService, ICartItemReadRepository cartItemReadRepository, ICartItemWriteRepository cartItemWriteRepository)
        {
            _cartService = cartService;
            _cartItemReadRepository = cartItemReadRepository;
            _cartItemWriteRepository = cartItemWriteRepository;
        }

        public async Task<RemoveCartItemResponse> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemReadRepository.GetByIdAsync(request.CartItemId);
            if (cartItem == null) return new RemoveCartItemResponse { Message = "Ürün bulunamadı" };

            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            if (cart == null || cart.Id != cartItem.CartId) return new RemoveCartItemResponse { Message = "Sepet bulunamadı" };

            var isOwner = await _cartService.ValidateCartOwnershipAsync(request.UserId,cart,request.SessionId,cancellationToken);
            if(!isOwner) return new RemoveCartItemResponse { Message = "Bu sepete erişiminiz bulunamamaktadır." };

             _cartItemWriteRepository.Remove(cartItem);

            await _cartItemWriteRepository.SaveChangesAsync();

            return new RemoveCartItemResponse() { Message = "Silme işlemi başarılı." };


        }
    }
}
