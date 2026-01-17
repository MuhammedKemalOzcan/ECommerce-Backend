using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Orders
{
    public record PaymentInfo
    {
        private PaymentInfo(string paymentId, string paymentType, int installment, string cardAssociation, string cardFamily, string cardLastFourDigits, string cardHolderName)
        {
            PaymentId = paymentId;
            PaymentType = paymentType;
            Installment = installment;
            CardAssociation = cardAssociation;
            CardFamily = cardFamily;
            CardLastFourDigits = cardLastFourDigits;
            CardHolderName = cardHolderName;
        }
        public string PaymentId { get; init; }
        public string PaymentType { get; init; }
        public int Installment { get; init; }
        public string CardAssociation { get; init; }
        public string CardFamily { get; init; }
        public string CardLastFourDigits { get; init; }
        public string CardHolderName { get; set; }

        public static PaymentInfo Create(string paymentId, string paymentType, int installment, string cardAssociation, string cardFamily, string cardLastFourDigits, string cardHolderName)
        {
            if (string.IsNullOrEmpty(paymentId)) throw new DomainException("Payment Id cannot be null");
            if (installment < 1) throw new DomainException("Installment must be at least 1.");
            if (string.IsNullOrEmpty(paymentType)) throw new DomainException("Payment type cannot be null");
            if (string.IsNullOrEmpty(cardAssociation)) throw new DomainException("Card association cannot be null");
            if (string.IsNullOrEmpty(cardFamily)) throw new DomainException("Card family cannot be null");
            if (string.IsNullOrEmpty(cardLastFourDigits)) throw new DomainException("Card last four digit cannot be null");
            if (string.IsNullOrEmpty(cardHolderName)) throw new DomainException("Card holder name cannot be null");

            return new PaymentInfo(paymentId, paymentType, installment, cardAssociation, cardFamily, cardLastFourDigits, cardHolderName);
        }
    }
}
