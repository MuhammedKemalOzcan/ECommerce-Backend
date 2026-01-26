using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Mappings;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Cart;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.MergeCarts
{
    public class MergeCartsCommandHandler : IRequestHandler<MergeCartsCommandRequest, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MergeCartsCommandHandler(IUnitOfWork unitOfWork, ICartRepository cartRepository)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> Handle(MergeCartsCommandRequest request, CancellationToken cancellationToken)
        {
            var guestCart = await _cartRepository.GetActiveCartAsync(null, request.SessionId);
            var userCart = await _cartRepository.GetActiveCartAsync(request.UserId, null);

            if (guestCart == null)
            {
                if (userCart is null)
                {
                    userCart = Cart.Create(request.UserId, null);
                    _cartRepository.Add(userCart);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                return userCart.ToDto();
            }

            if (userCart == null)
            {
                userCart = Cart.Create(request.UserId, null);
            }

            userCart.MergeCarts(guestCart);

            _cartRepository.Remove(guestCart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var cartDto = userCart.ToDto();
            return cartDto;
        }
    }
}
