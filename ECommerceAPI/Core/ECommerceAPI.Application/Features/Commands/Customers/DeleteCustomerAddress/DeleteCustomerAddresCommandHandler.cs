using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.Addresses;
using ECommerceAPI.Application.Repositories.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Customers.DeleteCustomerAddress
{
    public class DeleteCustomerAddresCommandHandler : IRequestHandler<DeleteCustomerAddresCommandRequest, DeleteCustomerAddresCommandResponse>
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IAddressesWriteRepository _addressesWriteRepository;
        private readonly IAddressesReadRepository _addressesReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<DeleteCustomerAddresCommandHandler> _logger;

        public DeleteCustomerAddresCommandHandler(ICustomerReadRepository customerReadRepository, IHttpContextAccessor contextAccessor, ILogger<DeleteCustomerAddresCommandHandler> logger, IAddressesWriteRepository addressesWriteRepository, IAddressesReadRepository addressesReadRepository)
        {
            _customerReadRepository = customerReadRepository;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _addressesWriteRepository = addressesWriteRepository;
            _addressesReadRepository = addressesReadRepository;
        }

        public async Task<DeleteCustomerAddresCommandResponse> Handle(DeleteCustomerAddresCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _addressesReadRepository.GetByIdAsync(request.addressId, false);

            if (address == null) throw new NotFoundException("Tanımlı adres bulunamadı");

            _addressesWriteRepository.Remove(address);
            await _addressesWriteRepository.SaveChangesAsync();

            return new DeleteCustomerAddresCommandResponse
            {
                Message = "Adres başarıyla silindi."
            };

        }
    }
}
