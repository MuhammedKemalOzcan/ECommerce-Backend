using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.RemoveAddress
{
    internal sealed class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAddressCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            var appUserId = _currentUserService.GetCurrentUserId();
            var customer = await _customerRepository.GetByUserIdAsync(appUserId);

            if (customer == null) throw new NotFoundException("User cannot be found.");

            customer.RemoveAddress(request.CustomerAddressId);

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
