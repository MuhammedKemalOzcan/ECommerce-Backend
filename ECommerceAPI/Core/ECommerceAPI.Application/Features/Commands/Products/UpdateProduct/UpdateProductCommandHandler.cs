﻿using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepo;
        private readonly IProductReadRepository _productReadRepo;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepo, IProductReadRepository productReadRepo)
        {
            _productWriteRepo = productWriteRepo;
            _productReadRepo = productReadRepo;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepo.GetByIdAsync(request.Id,true);

            if (product == null) throw new Exception("Ürün bulunamadı.");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Description = request.Description;
            product.Features = request.Features;

            _productWriteRepo.Update(product);
            await _productWriteRepo.SaveChangesAsync();

            return new UpdateProductCommandResponse
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                Features = product.Features
            };


        }
    }
}
