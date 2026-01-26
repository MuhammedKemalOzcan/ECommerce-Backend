using ECommerceAPI.Application.Dtos.Cart;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Cart;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AddItemToCartCommandHandler> _logger;

        public AddItemToCartCommandHandler(ICartRepository cartRepository, IProductRepository productRepository, ILogger<AddItemToCartCommandHandler> logger, IUnitOfWork uow)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _logger = logger;
            _uow = uow;
        }

        public async Task<CartDto> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartAsync(request.UserId, request.SessionId);

            if (cart == null)
            {
                cart = Cart.Create(request.UserId, request.SessionId);
                _cartRepository.Add(cart);
            }


            var product = await _productRepository.GetByIdOrThrowAsync(new ProductId(request.ProductId));

            var productImage = product.GetPrimaryImage();

            cart.AddCartItem(
                product.Id,
                request.Quantity,
                product.Price,
                product.Name,
                productImage
                );

            await _uow.SaveChangesAsync(cancellationToken);


            return new CartDto
            {
                Id = cart.Id.Value,
                TotalAmount = cart.TotalAmount,
                CartItems = cart.CartItems.Select(x => new CartItemDto
                {
                    Id = x.Id.Value,
                    ProductId = x.ProductId.Value,
                    ProductImageUrl = x.ProductImageUrl,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    UnitPrice = x.UnitPrice,
                }).ToList(),
                TotalItemCount = cart.TotalItemCount,
                UserId = cart.UserId
            };
        }
    }
}
