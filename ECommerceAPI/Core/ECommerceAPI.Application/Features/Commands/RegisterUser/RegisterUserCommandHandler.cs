using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.UserDto;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Customer;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IIdentityService _identity;
        private readonly IUnitOfWork _uow;
        private readonly ICustomerRepository _customerRepository;


        public RegisterUserCommandHandler(IIdentityService identity, ISharedIdentityService sharedIdentityService, IUnitOfWork uow, ICustomerRepository customerRepository)
        {
            _identity = identity;
            _uow = uow;
            _customerRepository = customerRepository;
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

            if (!Guid.TryParse(response.UserId, out Guid appUserId))
            {
                throw new Exception("Identity ID Guid formatında değil!");
            }

            var customer = Customer.Create(appUserId, request.Name, request.LastName, request.Email, request.PhoneNumber);

            _customerRepository.Add(customer);
            await _uow.SaveChangesAsync();


            return new()
            {
                Error = response.Error,
                Succeed = response.Succeed,
                Token = response.AccessToken
            };

        }
    }
}
