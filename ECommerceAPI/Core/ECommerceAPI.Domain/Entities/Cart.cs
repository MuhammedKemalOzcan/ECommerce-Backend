using ECommerceAPI.Domain.Enums;

namespace ECommerceAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; } // Guest sepeti için.
        public DateTime? ExpiryDate { get; set; }
        public CartStatus Status { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public decimal TotalAmount => CartItems?.Sum(x => x.TotalPrice) ?? 0;
        public int TotalItemCount => CartItems?.Sum(x => x.Quantity) ?? 0;

    }
}
