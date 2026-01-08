namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class PaymentAddressDto
    {
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}