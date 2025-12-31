using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => new ProductId(value)
                );

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Features)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Stock).IsRequired();

            builder.Property(p => p.Category).IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(155);


        }
    }
}
