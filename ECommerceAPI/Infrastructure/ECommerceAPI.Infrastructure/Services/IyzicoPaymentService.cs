using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Exceptions;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Threading;

namespace ECommerceAPI.Infrastructure.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICartService _cartService;

        public IyzicoPaymentService(IConfiguration configuration, ICurrentUserService currentUserService, ICartService cartService)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
            _cartService = cartService;
        }

        public async Task<PaymentResultDto> ReceivePaymentAsync(PaymentRequestDto paymentModel)
        {

            var userId = _currentUserService.GetCurrentUserId();

            var cart = await _cartService.GetActiveCartAsync(userId, null);

            if (cart == null)
            {
                throw new NotFoundException($"Cart not exist");
            }

            Options options = new Options();
            options.ApiKey = _configuration["Iyzico:ApiKey"];
            options.SecretKey = _configuration["Iyzico:SecretKey"];
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = paymentModel.Price.ToString("F2", CultureInfo.InvariantCulture);
            request.PaidPrice = paymentModel.PaidPrice.ToString("F2", CultureInfo.InvariantCulture);
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = cart.Id.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = paymentModel.CardHolderName;
            paymentCard.CardNumber = paymentModel.CardNumber;
            paymentCard.ExpireMonth = paymentModel.ExpireMonth;
            paymentCard.ExpireYear = paymentModel.ExpireYear;
            paymentCard.Cvc = paymentModel.Cvc;
            paymentCard.RegisterCard = 0; //Kartı kaydetmek için.
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = paymentModel.Buyer.Id;
            buyer.Name = paymentModel.Buyer.Name;
            buyer.Surname = paymentModel.Buyer.Surname;
            buyer.GsmNumber = paymentModel.Buyer.GsmNumber;
            buyer.Email = paymentModel.Buyer.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.RegistrationAddress = paymentModel.Buyer.RegistrationAddress;
            buyer.Ip = "85.34.78.112";
            buyer.City = paymentModel.Buyer.City;
            buyer.Country = paymentModel.Buyer.Country;
            buyer.ZipCode = paymentModel.Buyer.ZipCode;
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = paymentModel.ShippingAddress.ContactName;
            shippingAddress.City = paymentModel.ShippingAddress.City;
            shippingAddress.Country = paymentModel.ShippingAddress.Country;
            shippingAddress.Description = paymentModel.ShippingAddress.Street;
            shippingAddress.ZipCode = paymentModel.ShippingAddress.ZipCode;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = paymentModel.BillingAddress.ContactName;
            billingAddress.City = paymentModel.BillingAddress.City; ;
            billingAddress.Country = paymentModel.BillingAddress.Country;
            billingAddress.Description = paymentModel.BillingAddress.Street;
            billingAddress.ZipCode = paymentModel.BillingAddress.ZipCode;
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var item in paymentModel.BasketItems)
            {
                BasketItem basketItem = new BasketItem();
                basketItem.Id = item.Id;
                basketItem.Name = item.Name;
                basketItem.Category1 = item.Category1;
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                // Iyzico Price'ı string ister ve nokta ile ayrılmış olmalı (10.50)
                basketItem.Price = item.Price.ToString("F2", CultureInfo.InvariantCulture);
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            Payment payment = await Payment.Create(request, options);

            return new PaymentResultDto
            {
                IsSuccess = payment.Status == "success",
                PaymentId = payment.PaymentId,
                ErrorMessage = payment.ErrorMessage,
                CardFamily = payment.CardFamily,
                CardAssociation = payment.CardAssociation,
                CardType = payment.CardType,
                CardLastFourDigits = payment.LastFourDigits
            };
        }
    }
}
