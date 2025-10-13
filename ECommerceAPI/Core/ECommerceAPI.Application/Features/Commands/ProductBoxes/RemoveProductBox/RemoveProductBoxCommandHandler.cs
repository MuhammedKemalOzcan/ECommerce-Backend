using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.RemoveProductBox
{
    public class RemoveProductBoxCommandHandler : IRequestHandler<RemoveProductBoxCommandRequest, RemoveProductBoxCommandResponse>
    {
        private readonly IProductBoxReadRepository _readRepo;
        private readonly IProductBoxWriteRepository _writeRepo;

        public RemoveProductBoxCommandHandler(IProductReadRepository productReadRepository, IProductBoxReadRepository readRepo, IProductBoxWriteRepository writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        public async Task<RemoveProductBoxCommandResponse> Handle(RemoveProductBoxCommandRequest request, CancellationToken cancellationToken)
        {
            var productBox = await _readRepo.GetByIdAsync(request.BoxId,true);
            if (productBox == null) return new RemoveProductBoxCommandResponse { Message = $"{request.BoxId} id'li ürün içeriği bulunamadı" };

            _writeRepo.Remove(productBox);
            await _writeRepo.SaveChangesAsync();

            return new RemoveProductBoxCommandResponse { Message = "Ürün içeriği Başarıyla Silindi." };

        }
    }
}
