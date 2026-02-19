using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace ECommerceAPI.Infrastructure.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICartRepository _cartRepository;
        private readonly Iyzipay.Options _options;

        public IyzicoPaymentService(IConfiguration configuration, ICurrentUserService currentUserService, ICartRepository cartRepository, Options options)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
            _cartRepository = cartRepository;
            _options = options;
        }

        public async Task<PaymentInitializeResult> InitializeCheckoutFormAsync(CreateCheckoutFormDto formModel)
        {
            var userId = _currentUserService.GetCurrentUserId();

            var cart = await _cartRepository.GetActiveCartAsync(userId, null);

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

        public async Task<PaymentResultDto> ProcessCallbackAsync(string token)
        {
            var request = new RetrieveCheckoutFormRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Token = token;

            CheckoutForm checkoutForm = await CheckoutForm.Retrieve(request, _options);

            return new PaymentResultDto
            {
                CardAssociation = checkoutForm.CardAssociation,
                CardFamily = checkoutForm.CardFamily,
                CardLastFourDigits = checkoutForm.LastFourDigits,
                CardType = checkoutForm.CardType,
                ErrorMessage = checkoutForm.ErrorMessage,
                IsSuccess = checkoutForm.PaymentStatus == "SUCCESS",
                PaymentId = checkoutForm.PaymentId,
            };
        }
    }
}