using ECommerceAPI.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    internal class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                id => id.Value,
                value => new CustomerAddressId(value)
                );

            builder.Property(x => x.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(x => x.Location, locationBuilder =>
            {
                locationBuilder.Property(l => l.City)
                .HasMaxLength(50)
                .IsRequired();

                locationBuilder.Property(l => l.Street)
                .HasMaxLength(100)
                .IsRequired();

                locationBuilder.Property(l => l.ZipCode)
                .HasMaxLength(50)
                .IsRequired();

                locationBuilder.Property(l => l.Country)
                .HasMaxLength(25)
                .IsRequired();
            });

        }
    }
}
