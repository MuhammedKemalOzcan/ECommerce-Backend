using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.UserDto;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.LoginUser
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IIdentityService _identity;

        public LoginUserCommandHandler(IIdentityService identity)
        {
            _identity = identity;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AuthResultDto response = await _identity.LoginAsync(new()
            {
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
