using ECommerceAPI.Application.Dtos;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenDto>;
}
