using ECommerceAPI.Application.Dtos.PaymentDto;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.Iyzicocallback
{
    public record IyzicoCallbackCommand(string Token) : IRequest<CallbackResultDto>;
}