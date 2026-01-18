using ECommerceAPI.Application.Dtos.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UploadImage
{
    public record UploadImageCommand(Guid ProductId, List<UploadImageDto> Files) : IRequest<ProductDto>;
}
