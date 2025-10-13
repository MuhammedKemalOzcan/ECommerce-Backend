using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.UpdateProductBox
{
    public class UpdateBoxCommandRequest : IRequest<UpdateBoxCommandResponse>
    {
        public Guid BoxId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
