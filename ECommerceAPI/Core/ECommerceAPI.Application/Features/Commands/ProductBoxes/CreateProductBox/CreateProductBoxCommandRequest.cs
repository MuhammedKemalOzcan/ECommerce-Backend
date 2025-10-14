using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.CreateProductBox
{
    public class CreateProductBoxCommandRequest : IRequest<CreateProductBoxCommandResponse>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
