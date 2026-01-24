using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace ECommerceAPI.Infrastructure.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICartService _cartService;
        private readonly Iyzipay.Options _options;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<IyzicoPaymentService> _logger;
        public IyzicoPaymentService(IConfiguration configuration, ICurrentUserService currentUserService, ICartService cartService, IOrderRepository orderRepository, IUnitOfWork uow, ICustomerRepository customerRepository, ILogger<IyzicoPaymentService> logger)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
            _cartService = cartService;
            _options = new Iyzipay.Options();
            _options.ApiKey = _configuration["Iyzico:ApiKey"];
            _options.SecretKey = _configuration["Iyzico:SecretKey"];
            _options.BaseUrl = "https://sandbox-api.iyzipay.com";
            _orderRepository = orderRepository;
            _uow = uow;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<PaymentInitializeResult> InitializeCheckoutFormAsync(CreateCheckoutFormDto formModel)
        {

            var userId = _currentUserService.GetCurrentUserId();

            var cart = await _cartService.GetActiveCartAsync(userId, null);

            if (cart == null)
            {
                throw new NotFoundException($"Cart not exist");
            }

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = formModel.ConversationId;
            request.Price = formModel.Price.ToString("F2", CultureInfo.InvariantCulture);
            request.PaidPrice = formModel.PaidPrice.ToString("F2", CultureInfo.InvariantCulture);
            request.Currency = Currency.TRY.ToString();
            request.BasketId = cart.Id.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = _configuration["Iyzico:CallbackUrl"];

            List<int> enabledInstallments = new List<int>();
            enabledInstallments.Add(2);
            enabledInstallments.Add(3);
            enabledInstallments.Add(6);
            enabledInstallments.Add(9);
            request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new Buyer();
            buyer.Id = formModel.Buyer.Id;
            buyer.Name = formModel.Buyer.Name;
            buyer.Surname = formModel.Buyer.Surname;
            buyer.GsmNumber = formModel.Buyer.GsmNumber;
            buyer.Email = formModel.Buyer.Email;
            buyer.IdentityNumber = "11111111111";
            buyer.RegistrationAddress = formModel.Buyer.RegistrationAddress;
            buyer.Ip = "85.34.78.112";
            buyer.City = formModel.Buyer.City;
            buyer.Country = formModel.Buyer.Country;
            buyer.ZipCode = formModel.Buyer.ZipCode;
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = formModel.ShippingAddress.ContactName;
            shippingAddress.City = formModel.ShippingAddress.City;
            shippingAddress.Country = formModel.ShippingAddress.Country;
            shippingAddress.Description = formModel.ShippingAddress.Street;
            shippingAddress.ZipCode = formModel.ShippingAddress.ZipCode;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = formModel.BillingAddress.ContactName;
            billingAddress.City = formModel.BillingAddress.City; ;
            billingAddress.Country = formModel.BillingAddress.Country;
            billingAddress.Description = formModel.BillingAddress.Street;
            billingAddress.ZipCode = formModel.BillingAddress.ZipCode;
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var item in formModel.BasketItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    BasketItem basketItem = new BasketItem();
                    basketItem.Id = item.Id + "_" + i;
                    basketItem.Name = item.Name;
                    basketItem.Category1 = item.Category1;
                    basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                    basketItem.Price = item.Price.ToString("F2", CultureInfo.InvariantCulture);

                    basketItems.Add(basketItem);
                }

            }

            request.BasketItems = basketItems;

            CheckoutFormInitialize checkoutFormInitialize = await CheckoutFormInitialize.Create(request, _options);

            if (checkoutFormInitialize.Status != "success")
            {
                throw new Exception($"Iyzico Başlatma Hatası: {checkoutFormInitialize.ErrorMessage}");
            }

            return new PaymentInitializeResult
            {
                HtmlContent = checkoutFormInitialize.CheckoutFormContent,
                Token = checkoutFormInitialize.Token
            };
        }

        public async Task<CallbackResultDto> ProcessCallbackAsync(string token)
        {

            var request = new RetrieveCheckoutFormRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Token = token;



            var order = await _orderRepository.GetByTokenAsync(token);

            if (order == null) throw new NotFoundException("Order not found");

            var customerId = order.CustomerId;

            var customer = await _customerRepository.GetById(customerId.Value);

            if (customer == null) throw new NotFoundException("Customer Not Found");

            CheckoutForm checkoutForm = await CheckoutForm.Retrieve(request, _options);

            if (checkoutForm.PaymentStatus == "SUCCESS")
            {
                var paymentInfo = PaymentInfo.Create(
                    checkoutForm.PaymentId,
                    "CREDIT_CARD",
                    1,
                    checkoutForm.CardAssociation,
                    checkoutForm.CardFamily,
                    checkoutForm.LastFourDigits,
                    customer.FirstName + " " + customer.LastName
                );

                order.SetPaymentSuccess(paymentInfo);

            }

            await _uow.SaveChangesAsync();

            return new CallbackResultDto
            {
                IsSuccess = true,
                OrderCode = order.OrderCode
            };
        }
    }
}
