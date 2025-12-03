using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.Products;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.RemoveProductBox
{
    public class RemoveProductBoxCommandHandler : IRequestHandler<RemoveProductBoxCommandRequest, RemoveProductBoxCommandResponse>
    {
        private readonly IProductBoxReadRepository _readRepo;
        private readonly IProductBoxWriteRepository _writeRepo;
        private readonly ILogger<RemoveProductBoxCommandHandler> _logger;

        public RemoveProductBoxCommandHandler(IProductReadRepository productReadRepository, IProductBoxReadRepository readRepo, IProductBoxWriteRepository writeRepo, ILogger<RemoveProductBoxCommandHandler> logger)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _logger = logger;
        }

        public async Task<RemoveProductBoxCommandResponse> Handle(RemoveProductBoxCommandRequest request, CancellationToken cancellationToken)
        {
            var productBox = await _readRepo.GetByIdAsync(request.BoxId, true);
            if (productBox == null)
            {
                _logger.LogWarning("Ürün içeriği bulunamadı BoxId: {BoxId}", request.BoxId);
                throw new NotFoundException($"{request.BoxId} id'li ürün içeriği bulunamadı");
            }



            _writeRepo.Remove(productBox);
            await _writeRepo.SaveChangesAsync();

            return new RemoveProductBoxCommandResponse { Message = "Ürün içeriği Başarıyla Silindi." };

        }
    }
}
