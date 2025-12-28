namespace ECommerceAPI.Application.Dtos.Customer
{
    public class AddAddressDto
    {
        public string Title { get; set; }
        public LocationDto Location { get; set; }
        public bool IsPrimary { get; set; }
    }
}
