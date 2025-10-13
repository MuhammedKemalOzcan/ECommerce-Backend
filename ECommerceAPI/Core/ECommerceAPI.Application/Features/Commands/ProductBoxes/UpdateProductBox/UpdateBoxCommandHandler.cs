using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using MediatR;
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

        public UpdateBoxCommandHandler(IProductBoxReadRepository boxReadRepo, IProductBoxWriteRepository boxWriteRepo)
        {
            _boxReadRepo = boxReadRepo;
            _boxWriteRepo = boxWriteRepo;
        }

        public async Task<UpdateBoxCommandResponse> Handle(UpdateBoxCommandRequest request, CancellationToken cancellationToken)
        {
            var productBox = await _boxReadRepo.GetByIdAsync(request.BoxId,true);
            if (productBox == null) throw new NotFoundException("Ürün içeriği bulunamadı!");

            productBox.Name = request.Name;
            productBox.Quantity = request.Quantity;

             _boxWriteRepo.Update(productBox);
            await _boxWriteRepo.SaveChangesAsync();

            return new UpdateBoxCommandResponse
            {
                Name = productBox.Name,
                Quantity = productBox.Quantity
            };


        }
    }
}
