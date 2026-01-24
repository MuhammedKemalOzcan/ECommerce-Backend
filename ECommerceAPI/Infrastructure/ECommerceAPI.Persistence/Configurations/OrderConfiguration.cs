using ECommerceAPI.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                id => id.Value,
                value => new OrderId(value)
                );

            builder.Property(o => o.CustomerId)
                .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
                );

            builder.Property(o => o.OrderDate).IsRequired();

            builder.Property(o => o.GrandTotal).IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(o => o.OrderCode).IsRequired().HasMaxLength(50);

            builder.Property(o => o.SubTotal).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(o => o.ShippingCost).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(o => o.TaxAmount).IsRequired().HasColumnType("decimal(18,2)");

            builder.OwnsOne(o => o.ShippingAddress, shippingAddressBuilder =>
            {
                shippingAddressBuilder.Property(l => l.City)
                .HasMaxLength(50)
                .IsRequired();

                shippingAddressBuilder.Property(l => l.Street)
                .HasMaxLength(100)
                .IsRequired();

                shippingAddressBuilder.Property(l => l.ZipCode)
                .HasMaxLength(50)
                .IsRequired();

                shippingAddressBuilder.Property(l => l.Country)
                .HasMaxLength(50)
                .IsRequired();
            });

            builder.OwnsOne(o => o.BillingAddress, billingAddressBuilder =>
            {
                billingAddressBuilder.Property(l => l.City)
                .HasMaxLength(50)
                .IsRequired();

                billingAddressBuilder.Property(l => l.Street)
                .HasMaxLength(100)
                .IsRequired();

                billingAddressBuilder.Property(l => l.ZipCode)
                .HasMaxLength(50)
                .IsRequired();

                billingAddressBuilder.Property(l => l.Country)
                .HasMaxLength(50)
                .IsRequired();
            });

            builder.OwnsOne(o => o.PaymentInfo, paymentInfoBuilder =>
            {
                paymentInfoBuilder
                .Property(p => p.Installment);

                paymentInfoBuilder
                .Property(p => p.CardAssociation)
                .IsRequired(false)
                .HasMaxLength(50);

                paymentInfoBuilder
                .Property(p => p.PaymentType)
                .IsRequired(false)
                .HasMaxLength(50);

                paymentInfoBuilder
                .Property(p => p.CardFamily)
                .IsRequired(false)
                .HasMaxLength(50);

                paymentInfoBuilder
                .Property(p => p.CardLastFourDigits)
                .IsRequired(false)
                .HasMaxLength(50);
            });

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
