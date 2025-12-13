using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace ECommerceAPI.Application.Features.Commands.Customers.AddAddressToCustomer
{
    public class AddAddressToCustomerCommandHandler : IRequestHandler<AddAddressToCustomerCommandRequest, AddAddressToCustomerCommandResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AddAddressToCustomerCommandHandler> _logger;

        public AddAddressToCustomerCommandHandler(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository, IHttpContextAccessor httpContextAccessor, ILogger<AddAddressToCustomerCommandHandler> logger)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<AddAddressToCustomerCommandResponse> Handle(AddAddressToCustomerCommandRequest request, CancellationToken cancellationToken)
        {

            var Id = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Id == null)
            {
                _logger.LogWarning("Kullanıcı Bulunamadı");
                throw new NotFoundException("Kullanıcı bulunamadı!");
            }

            var userId = Guid.Parse(Id);

            var customer = await _customerReadRepository.GetByUserIdAsync(userId, false, cancellationToken);


            if (customer == null)
            {
                _logger.LogWarning("Kullanıcı Bulunamadı");
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
