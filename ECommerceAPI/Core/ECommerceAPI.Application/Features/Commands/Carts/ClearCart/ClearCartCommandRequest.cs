using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Carts.ClearCart
{
    public class ClearCartCommandRequest : IRequest<ClearCartCommandResponse>
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
    }
}
