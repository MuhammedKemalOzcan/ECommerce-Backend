namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class PaymentResultDto
    {
        public bool IsSuccess { get; set; }
        public string PaymentId { get; set; }
        public string ErrorMessage { get; set; }
        public string CardFamily { get; set; }
        public string CardAssociation { get; set; }
        public string CardType { get; set; }
        public string CardLastFourDigits { get; set; }
    }
}
