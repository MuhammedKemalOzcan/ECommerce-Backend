using ECommerceAPI.Domain.Exceptions;

public record Location
{

    private Location(string street, string city, string country, string zipCode)
    {
        Street = street;
        City = city;
        Country = country;
        ZipCode = zipCode;
    }

    public string Street { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    public static Location Create(string street, string city, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainException("Street cannot be empty.");
        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City cannot be empty.");
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country cannot be empty.");
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new DomainException("Invalid ZipCode format.");

        return new Location(street, city, country, zipCode);
    }

}