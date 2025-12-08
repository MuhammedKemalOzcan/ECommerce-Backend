using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string street, string city, string country, string zipCode)
        {
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

    }
}
