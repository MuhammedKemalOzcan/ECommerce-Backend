using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class ProductBoxConfiguration : IEntityTypeConfiguration<ProductBox>
    {
        public void Configure(EntityTypeBuilder<ProductBox> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                id => id.Value,
                value => new BoxId(value)
                );

            builder.Property(b => b.ProductId)
                .HasConversion(
                id => id.Value,
                value => new ProductId(value)
                );


            builder.Property(b => b.Quantity).IsRequired();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne<Product>()
                .WithMany(p => p.ProductBoxes)
                .HasForeignKey(p => p.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
