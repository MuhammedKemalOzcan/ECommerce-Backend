namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class CreateCheckoutFormDto
    {
        public string ConversationId { get; set; }
        public string BasketId { get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public PaymentBuyerDto Buyer { get; set; }
        public PaymentAddressDto ShippingAddress { get; set; }
        public PaymentAddressDto BillingAddress { get; set; }
        public List<PaymentBasketItemDto> BasketItems { get; set; }
    }
}
