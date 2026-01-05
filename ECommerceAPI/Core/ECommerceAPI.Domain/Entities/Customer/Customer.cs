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
            if (appUserId == Guid.Empty) throw new DomainException("User Cannot be found");
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

        public void Update(string firstName, string lastName, string email, string phoneNumber)
        {
            if (string.IsNullOrEmpty(firstName)) throw new DomainException("First name cannot be null.");
            if (string.IsNullOrEmpty(lastName)) throw new DomainException("Last name cannot be null.");
            if (string.IsNullOrEmpty(email)) throw new DomainException("email cannot be null.");
            if (string.IsNullOrEmpty(phoneNumber)) throw new DomainException("Phone Number cannot be null.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;

        }

        public void AddAddress(CustomerAddressId addressId, Location location, string title, bool isPrimary)
        {
            if (string.IsNullOrEmpty(title)) throw new DomainException("Title cannot be null.");

            if (_addresses.Count == 0) isPrimary = true;

            if (isPrimary == true)
            {
                UnsetPrimaryAddress();
            }

            var address = new CustomerAddress(addressId, Id, title, location, isPrimary);

            _addresses.Add(address);
        }

        public void RemoveAddress(CustomerAddressId customerAddressId)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == customerAddressId);

            if (address == null) throw new DomainException("Address cannot found.");

            _addresses.Remove(address);

        }

        public void SetPrimaryAddress(CustomerAddressId addressId)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null) throw new DomainException("Address cannot be found.");

            if (address.IsPrimary) return;

            UnsetPrimaryAddress();

            address.IsPrimary = true;

        }

        private void UnsetPrimaryAddress()
        {
            var currentPrimary = _addresses.FirstOrDefault(a => a.IsPrimary == true);

            if (currentPrimary != null) currentPrimary.IsPrimary = false;
        }
    }
}

public record CustomerId(Guid Value);
