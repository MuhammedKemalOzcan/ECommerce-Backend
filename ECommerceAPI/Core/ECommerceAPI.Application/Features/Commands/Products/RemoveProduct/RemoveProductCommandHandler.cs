using ECommerceAPI.Application.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWrite;
        private readonly IProductReadRepository _productRead;

        public RemoveProductCommandHandler(IProductWriteRepository productWrite, IProductReadRepository productRead)
        {
            _productWrite = productWrite;
            _productRead = productRead;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRead.GetByIdAsync(request.Id);
            if (product == null) return new RemoveProductCommandResponse { Message = $"{request.Id} id'li ürün bulunamadı" };

            _productWrite.Remove(product);
            await _productWrite.SaveChangesAsync();

            return new RemoveProductCommandResponse()
            {
                Id = request.Id,
                Message = $"{request.Id} id'sine sahip ürün başarıyla silindi."
            };

        }
    }
}
