namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class PaymentBuyerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GsmNumber { get; set; }
        public string Email { get; set; }
        public string RegistrationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}