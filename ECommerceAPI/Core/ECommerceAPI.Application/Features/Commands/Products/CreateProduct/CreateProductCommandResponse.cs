using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandResponse
    {
        public ProductDto? Data { get; set; }
    }
}
