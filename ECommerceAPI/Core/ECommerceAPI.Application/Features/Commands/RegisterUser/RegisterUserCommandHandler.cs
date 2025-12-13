using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.UserDto;
using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IIdentityService _identity;
        private readonly ICustomerWriteRepository _customerWriteRepository;


        public RegisterUserCommandHandler(IIdentityService identity, ICustomerWriteRepository customerWriteRepository, ISharedIdentityService sharedIdentityService)
        {
            _identity = identity;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {


            AuthResultDto response = await _identity.RegisterAsync(new()
            {
                Name = request.Name,
                Surname = request.LastName,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
            });

            if (!response.Succeed)
            {
                return new()
                {
                    Succeed = false,
                    Error = response.Error
                };
            }
            try
            {
                if (string.IsNullOrEmpty(response.UserId)) throw new Exception("UserId Gelmedi");

                var customer = new Customer(
                    appUserId: Guid.Parse(response.UserId),
                    firstName: request.Name,
                    lastName: request.LastName,
                    email: request.Email,
                    phoneNumber: request.PhoneNumber
                    );
                await _customerWriteRepository.AddAsync(customer);
                await _customerWriteRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcı oluştu ama Müşteri profili oluşturulamadı: {ex.Message}");
            }

            return new()
            {
                Error = response.Error,
                Succeed = response.Succeed,
                Token = response.Token
            };

        }
    }
}
