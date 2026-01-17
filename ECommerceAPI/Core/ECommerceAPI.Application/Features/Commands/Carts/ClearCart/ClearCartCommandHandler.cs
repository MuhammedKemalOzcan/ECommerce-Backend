using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Exceptions;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommandRequest, ClearCartCommandResponse>
    {
        private readonly ICartService _cartService;
        private readonly ICartItemWriteRepository _cartItemWriteRepository;

        public ClearCartCommandHandler(ICartService cartService, ICartItemWriteRepository cartItemWriteRepository)
        {
            _cartService = cartService;
            _cartItemWriteRepository = cartItemWriteRepository;
        }

        public async Task<ClearCartCommandResponse> Handle(ClearCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetActiveCartAsync(request.UserId, request.SessionId, cancellationToken);
            if (cart == null) throw new NotFoundException("Sepet bulunamadı");

            var isOwner = await _cartService.ValidateCartOwnershipAsync(request.UserId, cart, request.SessionId, cancellationToken);
            if (!isOwner) throw new NotFoundException("Sepete erişiminiz bulunmamaktadır.");

            foreach (var item in cart.CartItems.ToList())
            {
                 _cartItemWriteRepository.Remove(item);
            }

            await _cartItemWriteRepository.SaveChangesAsync();

            return new ClearCartCommandResponse();

        }
    }
}
