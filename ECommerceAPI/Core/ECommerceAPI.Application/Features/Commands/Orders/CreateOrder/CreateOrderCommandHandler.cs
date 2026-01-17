using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly ICartsWriteRepository _cartsWriteRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public CreateOrderCommandHandler(ICartService cartService, ICurrentUserService currentUser, IUnitOfWork uow, IOrderRepository orderRepository, ICustomerRepository customerRepository, ILogger<CreateOrderCommandHandler> logger, ICartsWriteRepository cartsWriteRepository, IProductRepository productRepository, IPaymentService paymentService)
        {
            _cartService = cartService;
            _currentUser = currentUser;
            _uow = uow;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _logger = logger;
            _cartsWriteRepository = cartsWriteRepository;
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            foreach (var item in cart.CartItems)
            {
                if (item == null) throw new NotFoundException($"Item with ${item.Id} Id not exist");

                item.Product.DecreaseStock(item.Quantity);

                _productRepository.Update(item.Product);

                order.AddOrderItem(
                    item.ProductId,
                    item.Product.Name, //product cart içerisinde include ediliyor.
                    item.UnitPrice,
                    item.Quantity
                );
            }

            var basketItemsDto = cart.CartItems.Select(item => new PaymentBasketItemDto
            {
                Id = item.ProductId.ToString(),
                Name = item.Product.Name,
                Category1 = item.Product.Category,
                Price = item.UnitPrice,
            }).ToList();

            var paymentRequestDto = new PaymentRequestDto()
            {
                CardHolderName = request.PaymentInfo.CardHolderName,
                CardNumber = request.PaymentInfo.CardNumber,
                ExpireMonth = request.PaymentInfo.ExpireMonth,
                ExpireYear = request.PaymentInfo.ExpireYear,
                Cvc = request.PaymentInfo.Cvc,
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

            var iyzicoResult = await _paymentService.ReceivePaymentAsync(paymentRequestDto);

            if (!iyzicoResult.IsSuccess)
            {
                _logger.LogError($"Payment failed for User {userId}. Ödeme Hatası: {iyzicoResult.ErrorMessage}");
                throw new BusinessException($"Ödeme alınamadı, Ödeme Hatası: {iyzicoResult.ErrorMessage}");
            }


            var paymentInfo = PaymentInfo.Create(
                iyzicoResult.PaymentId,
                "CREDIT_CARD",
                request.installment,
                iyzicoResult.CardAssociation,
                iyzicoResult.CardFamily,
                iyzicoResult.CardLastFourDigits,
                request.PaymentInfo.CardHolderName
            );
            order.SetPaymentInfo(paymentInfo);



            _orderRepository.Add(order);
            try
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new BusinessException("Sipariş oluşturulurken ürün stok durumu değişti. Ödemeniz iade edildi.");
            }
            _logger.LogInformation("Order {OrderId} created successfully for user {UserId}", order.Id.Value, userId);

            return order.OrderCode;

        }
    }
}
