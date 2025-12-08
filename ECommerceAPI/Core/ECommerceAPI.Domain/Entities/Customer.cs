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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public bool isActive { get; private set; }

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();


        public Customer(Guid appUserId,string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            AppUserId = appUserId;
            LastName = lastName;
            Email = email;
            isActive = true;
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
