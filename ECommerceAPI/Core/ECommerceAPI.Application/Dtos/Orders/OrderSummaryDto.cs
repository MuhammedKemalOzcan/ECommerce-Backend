using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Domain.Enums;

namespace ECommerceAPI.Application.Dtos.Orders
{
    public class OrderSummaryDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string OrderCode { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal SubTotal { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<OrderItemsDto> OrderItems { get; set; }
        public LocationDto ShippingAddress { get; set; }
        public CustomerDto Customer { get; set; }
        public PaymentInfoDto PaymentInfo { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
