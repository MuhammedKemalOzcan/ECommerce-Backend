namespace ECommerceAPI.Application.Dtos.Orders
{
    public class PaymentInfoDto
    {
        public string PaymentId { get; set; }
        public string PaymentType { get; set; }
        public int Installment { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string CardLastFourDigits { get; set; }
        public string? CardHolderName { get; set; }
    }
}
