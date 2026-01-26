using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.RemoveCartItem
{
    public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemRequest>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveCartItemHandler> _logger;

        public RemoveCartItemHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ILogger<RemoveCartItemHandler> logger)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository
                .GetActiveCartAsync(request.UserId, request.SessionId);

            if (cart is null) return;

            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id.Value == request.CartItemId);

            if (cartItem is null)
            {
                _logger.LogWarning($"Cart item with {request.CartItemId} Id cannot be found");
                throw new NotFoundException("Cart item cannot be found");
            }

            cart.RemoveCartItem(cartItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
