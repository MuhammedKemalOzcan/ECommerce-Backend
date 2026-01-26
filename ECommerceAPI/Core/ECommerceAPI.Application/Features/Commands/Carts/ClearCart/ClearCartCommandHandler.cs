using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommandRequest>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClearCartCommandHandler> _logger;

        public ClearCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ILogger<ClearCartCommandHandler> logger)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(ClearCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartAsync(request.UserId, request.SessionId);
            if (cart == null)
            {
                return;
            }

            cart.ClearCart();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
