namespace ECommerceAPI.Application.Dtos.Customer
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public LocationDto Location { get; set; }
        public bool IsPrimary { get; set; }
    }
}
