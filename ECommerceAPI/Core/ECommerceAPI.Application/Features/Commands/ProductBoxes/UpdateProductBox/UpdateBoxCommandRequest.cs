using MediatR;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.UpdateProductBox
{
    public class UpdateBoxCommandRequest : IRequest<UpdateBoxCommandResponse>
    {
        [JsonIgnore]
        public Guid BoxId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
