using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, ISharedIdentityService sharedIdentityService)
        {
            _customerWriteRepository = customerWriteRepository;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _sharedIdentityService.GetUserId();
            var appUserId = Guid.Parse(userId);

            var customer = new Customer(
                appUserId: appUserId,
                firstName: request.FirstName,
                lastName: request.LastName,
                email: request.Email
                );

            await _customerWriteRepository.AddAsync(customer);
            await _customerWriteRepository.SaveChangesAsync();

            return new CreateCustomerCommandResponse
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,

            };
        }
    }
}
