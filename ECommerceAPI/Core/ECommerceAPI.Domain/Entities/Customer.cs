using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();


        public Customer(Guid appUserId, string firstName, string lastName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            AppUserId = appUserId;
            LastName = lastName;
            Email = email;
            IsActive = true;
            PhoneNumber = phoneNumber;
        }

        public void AddAddress(string street, string city, string country, string zipCode)
        {
            if (_addresses.Count > 3)
            {
                throw new Exception("A customer cannot have more than 4 addresses.");
            }
            var address = new Address(street, city, country, zipCode);
            _addresses.Add(address);
        }


    }
}
