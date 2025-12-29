using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.PrimaryAddress
{
    internal sealed class SetPrimaryAddressCommandHandler : IRequestHandler<SetPrimaryAddressCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public SetPrimaryAddressCommandHandler(ICustomerRepository customerRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(SetPrimaryAddressCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null) throw new NotFoundException("Customer cannot be found.");

            customer.SetPrimaryAddress(request.AddressId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
