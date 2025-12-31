using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class ProductGalleryConfiguration : IEntityTypeConfiguration<ProductGallery>
    {
        public void Configure(EntityTypeBuilder<ProductGallery> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasConversion(
                id => id.Value,
                value => new ImageId(value)
                );

            builder.Property(g => g.ProductId)
                .HasConversion(
                id => id.Value,
                value => new ProductId(value)
                );

            builder.Property(g => g.Storage).IsRequired().HasMaxLength(50);

            builder.Property(g => g.Path).IsRequired().HasMaxLength(500);

            builder.Property(g => g.FileName).IsRequired().HasMaxLength(255);

            builder.Property(g => g.IsPrimary).IsRequired();

            builder.HasOne<Product>()
                .WithMany(p => p.ProductGalleries)
                .HasForeignKey(p => p.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
