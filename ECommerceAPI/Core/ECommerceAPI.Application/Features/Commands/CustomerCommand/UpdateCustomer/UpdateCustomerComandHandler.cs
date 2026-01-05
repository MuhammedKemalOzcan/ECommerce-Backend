using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.CustomerCommand.UpdateCustomer
{
    public class UpdateCustomerComandHandler : IRequestHandler<UpdateCustomerComand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _uow;

        public UpdateCustomerComandHandler(ICustomerRepository customerRepository, IUnitOfWork uow, ICurrentUserService currentUserService)
        {
            _customerRepository = customerRepository;
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerComand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetCurrentUserId();

            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null) throw new NotFoundException("Customer cannot be found.");

            customer.Update(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            await _uow.SaveChangesAsync(cancellationToken);

            return new CustomerDto
            {
                Id = customer.Id.Value,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Addresses = customer.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id.Value,
                    Title = a.Title,
                    IsPrimary = a.IsPrimary,
                    Location = new LocationDto
                    {
                        City = a.Location.City,
                        Street = a.Location.Street,
                        Country = a.Location.Country,
                        ZipCode = a.Location.ZipCode
                    }
                }).ToList()
            };

        }
    }
}
