using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;


namespace ECommerceAPI.Application.Features.Commands.Customers.AddAddressToCustomer
{
    public class AddAddressToCustomerCommandHandler : IRequestHandler<AddAddressToCustomerCommandRequest, AddAddressToCustomerCommandResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public AddAddressToCustomerCommandHandler(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<AddAddressToCustomerCommandResponse> Handle(AddAddressToCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId,false);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            customer.AddAddress(request.Street, request.City, request.Country, request.ZipCode);

            await _customerWriteRepository.SaveChangesAsync();
            return new AddAddressToCustomerCommandResponse
            {
                CustomerId = customer.Id
            };


        }
    }
}
