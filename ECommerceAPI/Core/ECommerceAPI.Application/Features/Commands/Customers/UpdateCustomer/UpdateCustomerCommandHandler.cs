using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ECommerceAPI.Application.Features.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(ICustomerReadRepository customerReadRepository, IHttpContextAccessor contextAccessor, ILogger<UpdateCustomerCommandHandler> logger, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var Id = _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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
                throw new NotFoundException("Kullanıcı bulunamadı!");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;

            _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveChangesAsync();

            return new UpdateCustomerCommandResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };
        }
    }
}
