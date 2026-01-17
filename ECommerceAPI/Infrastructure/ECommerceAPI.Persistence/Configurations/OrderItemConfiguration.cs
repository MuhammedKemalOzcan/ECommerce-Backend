using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                id => id.Value,
                value => new OrderItemId(value)
                );
            builder.Property(o => o.OrderId)
                .HasConversion(
                id => id.Value,
                value => new OrderId(value)
                );
            builder.Property(o => o.ProductId)
                .HasConversion(
                id => id.Value,
                value => new ProductId(value)
                );


            builder.Property(o => o.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Price)
                .IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
