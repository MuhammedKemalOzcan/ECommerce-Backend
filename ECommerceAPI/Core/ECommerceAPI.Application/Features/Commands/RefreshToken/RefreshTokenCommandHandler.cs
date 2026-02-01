using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
            _identityService = identityService;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshToken(request.RefreshToken);

            if (refreshToken == null)
            {
                throw new UnauthorizedException("Invalid or expired refresh token.");
            }

            var user = await _identityService.RefreshTokenLoginAsync(refreshToken.UserId);

            return new TokenDto
            {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken
            };

        }
    }
}
