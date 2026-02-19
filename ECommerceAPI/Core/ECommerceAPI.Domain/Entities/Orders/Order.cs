using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Orders
{
    public class Order
    {
        private Order()
        { }

        public OrderId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public string OrderCode { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime UpdatedDate { get; set; }
        public DeliveryStatus Status { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal GrandTotal { get; private set; }
        public Location ShippingAddress { get; private set; }
        public Location BillingAddress { get; private set; }
        public PaymentInfo? PaymentInfo { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public string? PaymentToken { get; private set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public static Order Create(CustomerId customerId, decimal shippingCost, Location shippingAddress, Location billingAddress)
        {
            if (shippingCost < 0) throw new DomainException("Shipping cost cannot be negative");
            if (shippingAddress == null) throw new DomainException("Shipping address cannot be empty");
            if (billingAddress == null) throw new DomainException("Billing address cannot be empty");

            var order = new Order()
            {
                Id = new OrderId(Guid.NewGuid()),
                CustomerId = customerId,
                OrderCode = GenerateOrderCode(),
                OrderDate = DateTime.UtcNow,
                Status = DeliveryStatus.Pending,
                ShippingCost = shippingCost,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                PaymentInfo = null,
                PaymentStatus = PaymentStatus.Pending,
                SubTotal = 0,
                TaxAmount = 0,
                GrandTotal = 0,
            };

            return order;
        }

        public void SetPaymentSuccess(PaymentInfo paymentInfo)
        {
            if (PaymentStatus == PaymentStatus.Success)
                return;

            PaymentInfo = paymentInfo;
            PaymentStatus = PaymentStatus.Success;
        }

        public void SetPaymentToken(string token)
        {
            PaymentToken = token;
        }

        public void SetPaymentFailed()
        {
            PaymentStatus = PaymentStatus.Failed;
        }

        public void AddOrderItem(ProductId productId, string productName, decimal price, int quantity)
        {
            var orderItem = new OrderItem(Id, productId, productName, price, quantity);
            _orderItems.Add(orderItem);

            CalculateSubTotal();
            CalculateGrandTotal();
        }

        private void CalculateGrandTotal()
        {
            SubTotal = CalculateSubTotal();

            var taxRate = 0.2m;

            TaxAmount = SubTotal * taxRate;

            GrandTotal = SubTotal + TaxAmount + ShippingCost;
        }

        private decimal CalculateSubTotal()
        {
            SubTotal = _orderItems.Sum(x => x.Price * x.Quantity);
            return SubTotal;
        }

        public void CancelOrder()
        {
            if (Status == DeliveryStatus.Shipped || Status == DeliveryStatus.Delivered)
            {
                throw new DomainException("Kargoya verilmiş sipariş iptal edilemez. İade süreci başlatmalısınız.");
            }

            Status = DeliveryStatus.Canceled;
            UpdatedDate = DateTime.UtcNow;
        }

        public void ShipOrder()
        {
            if (Status == DeliveryStatus.Canceled) throw new DomainException("İptal edilmiş sipariş kargolanamaz");
            if (PaymentInfo == null) throw new DomainException("Ödeme yapılmamış sipariş kargolanamaz");
            if (Status == DeliveryStatus.Shipped || Status == DeliveryStatus.Delivered)
            {
                throw new DomainException("Sipariş zaten kargolanmış");
            }
            Status = DeliveryStatus.Shipped;
            ShippedDate = DateTime.UtcNow;
        }

        public void DeliverOrder()
        {
            if (Status == DeliveryStatus.Canceled) throw new DomainException("Ürün iptal edilmiş.");
            if (Status == DeliveryStatus.Delivered) throw new DomainException("Sipariş zaten teslim edilmiş");
            Status = DeliveryStatus.Delivered;
            DeliveredDate = DateTime.UtcNow;
        }

        private static string GenerateOrderCode()
        {
            var Random = new Random().Next(1000, 9999);
            return $"ORD_{DateTime.UtcNow.Year}{Random}";
        }

        public void SetPaymentInfo(PaymentInfo paymentInfo)
        {
            PaymentInfo = paymentInfo;
        }
    }
}