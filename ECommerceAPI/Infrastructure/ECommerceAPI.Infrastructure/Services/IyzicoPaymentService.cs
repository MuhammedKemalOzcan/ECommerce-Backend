using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Dtos.PaymentDto;
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

        public IyzicoPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ReceivePaymentAsync(PaymentRequestDto paymentModel)
        {
            Options options = new Options();
            options.ApiKey = _configuration["Iyzico:ApiKey"];
            options.SecretKey = _configuration["Iyzico:SecretKey"];
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = paymentModel.Price.ToString("F2");
            request.PaidPrice = paymentModel.Price.ToString("F2");
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
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
            buyer.IdentityNumber = paymentModel.Buyer.IdentityNumber;
            buyer.RegistrationAddress = paymentModel.Buyer.RegistrationAddress;
            buyer.Ip = paymentModel.Buyer.Ip;
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
            billingAddress.City = paymentModel.BillingAddress.ContactName; ;
            billingAddress.Country = paymentModel.BillingAddress.ContactName; ;
            billingAddress.Description = paymentModel.BillingAddress.ContactName;
            billingAddress.ZipCode = paymentModel.BillingAddress.ContactName;
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

            Payment payment = new Payment();

            if (payment.Status == "success")
            {
                return payment.PaymentId;
            }
            else
            {
                // Hata mesajını logla: payment.ErrorMessage
                throw new Exception($"Ödeme Başarısız: {payment.ErrorMessage}");
            }
        }
    }
}
