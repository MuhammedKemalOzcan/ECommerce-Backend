using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommand, string>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<InitializePaymentCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public InitializePaymentCommandHandler(ICurrentUserService currentUser, IUnitOfWork uow, IOrderRepository orderRepository, ICustomerRepository customerRepository, ILogger<InitializePaymentCommandHandler> logger, IProductRepository productRepository, IPaymentService paymentService, ICartRepository cartRepository)
        {
            _currentUser = currentUser;
            _uow = uow;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _logger = logger;
            _productRepository = productRepository;
            _paymentService = paymentService;
            _cartRepository = cartRepository;
        }

        public async Task<string> Handle(InitializePaymentCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetCurrentUserId();

            var cart = await _cartRepository.GetActiveCartAsync(userId, null);

            if (cart == null)
            {
                _logger.LogWarning($"Cart not exist");
                throw new NotFoundException($"Cart not exist");
            }

            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
            {
                _logger.LogWarning($"Customer not exist");
                throw new NotFoundException($"Customer not exist");
            }

            var productIds = cart.CartItems.Select(x => x.ProductId).ToList();

            var products = await _productRepository.GetProductsByIdsAsync(productIds);

            var productMap = products.ToDictionary(p => p.Id, p => p);

            var billingAddress = Location.Create(
            request.BillingAddress.Street,
            request.BillingAddress.City,
            request.BillingAddress.Country,
            request.BillingAddress.ZipCode
            );

            var shippingAddress = Location.Create(
            request.BillingAddress.Street,
            request.BillingAddress.City,
            request.BillingAddress.Country,
            request.BillingAddress.ZipCode
            );

            var order = Order.Create(customer.Id,
                request.ShippingCost, shippingAddress, billingAddress);

            foreach (var item in cart.CartItems)
            {
                if (item == null) throw new NotFoundException($"Item not exist");

                if (!productMap.TryGetValue(item.ProductId, out var product))
                {
                    throw new NotFoundException($"Product not found: {item.ProductName}");
                }

                order.AddOrderItem(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity
                );
            }

            _orderRepository.Add(order);
            await _uow.SaveChangesAsync(cancellationToken);

            var basketItemsDto = cart.CartItems.Select(item =>
            {
                var product = productMap[item.ProductId];
                return new PaymentBasketItemDto
                {
                    Id = item.ProductId.ToString(),
                    Quantity = item.Quantity,
                    Name = item.ProductName,
                    Category1 = product.Category,
                    Price = item.UnitPrice,
                };
            }).ToList();

            var newOrder = await _orderRepository.GetByIdAsync(order.Id);

            var checkoutFormDto = new CreateCheckoutFormDto()
            {
                ConversationId = order.Id.Value.ToString(),
                BasketId = cart.Id.ToString(),
                Price = order.SubTotal,
                PaidPrice = order.GrandTotal,
                Buyer = new PaymentBuyerDto()
                {
                    Id = customer.Id.ToString(),
                    Name = customer.FirstName,
                    Surname = customer.LastName,
                    GsmNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    RegistrationAddress = $"{shippingAddress.Street}, {shippingAddress.City}, {shippingAddress.Country}",
                    City = shippingAddress.City,
                    Country = shippingAddress.Country,
                    ZipCode = shippingAddress.ZipCode
                },
                BillingAddress = new PaymentAddressDto()
                {
                    ContactName = $"{customer.FirstName} {customer.LastName}",
                    City = billingAddress.City,
                    Country = billingAddress.Country,
                    Street = billingAddress.Street,
                    ZipCode = billingAddress.ZipCode
                },
                ShippingAddress = new PaymentAddressDto()
                {
                    ContactName = $"{customer.FirstName} {customer.LastName}",
                    City = shippingAddress.City,
                    Country = shippingAddress.Country,
                    Street = shippingAddress.Street,
                    ZipCode = shippingAddress.ZipCode
                },
                BasketItems = basketItemsDto
            };

            var htmlContent = await _paymentService.InitializeCheckoutFormAsync(checkoutFormDto);

            order.SetPaymentToken(htmlContent.Token);
            await _uow.SaveChangesAsync();

            return htmlContent.HtmlContent;
        }
    }
}