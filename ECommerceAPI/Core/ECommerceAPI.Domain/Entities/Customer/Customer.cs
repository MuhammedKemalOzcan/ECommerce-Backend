using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Customer
{
    public class Customer
    {
        private Customer() { }
        public CustomerId Id { get; private set; }
        public Guid AppUserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string PhoneNumber { get; private set; }

        private readonly List<CustomerAddress> _addresses = new();
        public IReadOnlyCollection<CustomerAddress> Addresses => _addresses.AsReadOnly();

        public static Customer Create(Guid appUserId, string firstName, string lastName, string email, string phoneNumber)
        {
            if(appUserId == Guid.Empty) throw new DomainException("User Cannot be found");
            if (string.IsNullOrEmpty(firstName)) throw new DomainException("First name cannot be null.");
            if (string.IsNullOrEmpty(lastName)) throw new DomainException("Last name cannot be null.");
            if (string.IsNullOrEmpty(email)) throw new DomainException("email cannot be null.");
            var customer = new Customer
            {
                Id = new CustomerId(Guid.NewGuid()),
                AppUserId = appUserId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            return customer;
        }

        public void AddAddress(CustomerAddressId addressId,Location location, string title,bool isPrimary)
        {
            if (string.IsNullOrEmpty(title)) throw new DomainException("Title cannot be null.");

            if (_addresses.Count == 0) isPrimary = true;

            if (isPrimary == true)
            {
                foreach (var existingAddress in _addresses)
                {
                    existingAddress.IsPrimary = false;
                }
            }

            var address = new CustomerAddress(addressId, Id, title, location, isPrimary);

            _addresses.Add(address);
        }
    }
}

public record CustomerId(Guid Value);
