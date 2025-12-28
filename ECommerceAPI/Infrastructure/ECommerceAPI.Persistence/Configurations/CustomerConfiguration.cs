using ECommerceAPI.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.AppUserId)
                .IsRequired();

            builder.HasIndex(c => c.AppUserId)
                .IsUnique();

            builder.Property(c => c.Id)
                .HasConversion(
                id => id.Value,
                value => new CustomerId(value));

            builder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.HasMany(c => c.Addresses)
                .WithOne()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Addresses)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
