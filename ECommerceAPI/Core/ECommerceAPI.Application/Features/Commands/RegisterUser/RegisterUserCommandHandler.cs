using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.UserDto;
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

        public RegisterUserCommandHandler(IIdentityService identity)
        {
            _identity = identity;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            AuthResultDto response = await _identity.RegisterAsync(new()
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password
            });

            return new()
            {
                Error = response.Error,
                Succeed = response.Succeed,
                Token = response.Token
            };
            
        }
    }
}
