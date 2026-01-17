using ECommerceAPI.Application.Dtos.Customer;

namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class PaymentRequestDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }

        public PaymentBuyerDto Buyer { get; set; }

        public PaymentAddressDto BillingAddress { get; set; }
        public PaymentAddressDto ShippingAddress { get; set; }
        public List<PaymentBasketItemDto> BasketItems { get; set; }
    }
}
