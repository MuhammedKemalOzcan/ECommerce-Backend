namespace ECommerceAPI.Application.Features.Commands.Customers.CreateCustomer
{
    public class CreateCustomerCommandResponse
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
