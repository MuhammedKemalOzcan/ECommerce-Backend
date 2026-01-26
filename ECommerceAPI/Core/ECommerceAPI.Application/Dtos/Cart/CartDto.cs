namespace ECommerceAPI.Application.Dtos.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string? SessionId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public int TotalItemCount { get; set; }
    }
}
