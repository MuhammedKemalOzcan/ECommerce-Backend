using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.AddAddress
{
    internal sealed class AddAddressToCustomerCommandHandler : IRequestHandler<AddAddressToCustomerCommandRequest,Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public AddAddressToCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(AddAddressToCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var appUserId = _currentUserService.GetCurrentUserId();

            var customer = await _customerRepository.GetByUserIdAsync(appUserId);

            if (customer == null) throw new NotFoundException("Customer cannot found.");

            var location = Location.Create(
                request.Location.Street,
                request.Location.City,
                request.Location.Country,
                request.Location.ZipCode
                );

            var newAddressId = new CustomerAddressId(Guid.NewGuid());

            customer.AddAddress(newAddressId,location, request.Title, request.IsPrimary);

            await _unitOfWork.SaveChangesAsync();

            return newAddressId.Value;
        }
    }
}
