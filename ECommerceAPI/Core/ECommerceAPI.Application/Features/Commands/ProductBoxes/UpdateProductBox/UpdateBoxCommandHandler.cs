using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.ProductBoxes.UpdateProductBox
{
    public class UpdateBoxCommandHandler : IRequestHandler<UpdateBoxCommandRequest, UpdateBoxCommandResponse>
    {
        private readonly IProductBoxReadRepository _boxReadRepo;
        private readonly IProductBoxWriteRepository _boxWriteRepo;
        private readonly ILogger<UpdateBoxCommandHandler> _logger;

        public UpdateBoxCommandHandler(IProductBoxReadRepository boxReadRepo, IProductBoxWriteRepository boxWriteRepo, ILogger<UpdateBoxCommandHandler> logger)
        {
            _boxReadRepo = boxReadRepo;
            _boxWriteRepo = boxWriteRepo;
            _logger = logger;
        }

        public async Task<UpdateBoxCommandResponse> Handle(UpdateBoxCommandRequest request, CancellationToken cancellationToken)
        {
            var productBox = await _boxReadRepo.GetByIdAsync(request.BoxId,true);
            if (productBox == null)
            {
                _logger.LogWarning("Ürün içeriği bulunamadı! BoxId: {BoxId}", request.BoxId);
                throw new NotFoundException("Ürün içeriği bulunamadı!");
            }

            productBox.Name = request.Name;
            productBox.Quantity = request.Quantity;

             _boxWriteRepo.Update(productBox);
            await _boxWriteRepo.SaveChangesAsync();

            return new UpdateBoxCommandResponse
            {
                Id = productBox.Id,
                Name = productBox.Name,
                Quantity = productBox.Quantity
            };


        }
    }
}
