using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Customer
{
    public class CustomerAddress
    {
        private CustomerAddress() { }
        internal CustomerAddress(CustomerAddressId id, CustomerId customerId, string title, Location location, bool isPrimary)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Title cannot be empty");
            if (location == null) throw new DomainException("Address cannot be empty");
            Id = id;
            CustomerId = customerId;
            Title = title;
            Location = location;
            IsPrimary = isPrimary;
        }
        public CustomerAddressId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public string Title { get; private set; }
        public Location Location { get; private set; }
        public bool IsPrimary { get; internal set; }
    }
}

public record CustomerAddressId(Guid Value);
