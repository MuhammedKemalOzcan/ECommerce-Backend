using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommand, string>
    {
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<InitializePaymentCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public InitializePaymentCommandHandler(ICartService cartService, ICurrentUserService currentUser, IUnitOfWork uow, IOrderRepository orderRepository, ICustomerRepository customerRepository, ILogger<InitializePaymentCommandHandler> logger, IProductRepository productRepository, IPaymentService paymentService)
        {
            _cartService = cartService;
            _currentUser = currentUser;
            _uow = uow;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _logger = logger;
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        public async Task<string> Handle(InitializePaymentCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetCurrentUserId();

            var cart = await _cartService.GetActiveCartAsync(userId, null, cancellationToken);

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

            var billingAddress = Location.Create(
            request.BillingAddress.City,
            request.BillingAddress.Country,
            request.BillingAddress.Street,
            request.BillingAddress.ZipCode
            );

            var shippingAddress = Location.Create(
            request.ShippingAddress.City,
            request.ShippingAddress.Country,
            request.ShippingAddress.Street,
            request.ShippingAddress.ZipCode
            );

            var order = Order.Create(customer.Id,
                request.ShippingCost, shippingAddress, billingAddress);

            _logger.LogError($"[DEBUG] 1. Yeni Order Oluştu. ID: {order.Id.Value}");
            _logger.LogError($"[DEBUG] 1. Bu Order için Cart ID: {cart.Id}");

            foreach (var item in cart.CartItems)
            {
                if (item == null) throw new NotFoundException($"Item with ${item.Id} Id not exist");

                item.Product.DecreaseStock(item.Quantity);

                _productRepository.Update(item.Product);

                order.AddOrderItem(
                    item.ProductId,
                    item.Product.Name,
                    item.UnitPrice,
                    item.Quantity
                );
            }

            _orderRepository.Add(order);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogError($"[DEBUG] 2. SaveChanges Sonrası Order ID: {order.Id.Value}");

            var basketItemsDto = cart.CartItems.Select(item => new PaymentBasketItemDto
            {
                Id = item.ProductId.ToString(),
                Quantity = item.Quantity,
                Name = item.Product.Name,
                Category1 = item.Product.Category,
                Price = item.UnitPrice,
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
