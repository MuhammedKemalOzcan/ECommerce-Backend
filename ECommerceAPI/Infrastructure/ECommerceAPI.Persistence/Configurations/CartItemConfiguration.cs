using ECommerceAPI.Domain.Entities.Cart;
using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CartItemId(value));

            builder.Property(ci => ci.CartId)
                .HasConversion(
                    id => id.Value,
                    value => new CartId(value));

            builder.Property(ci => ci.ProductId)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));

            builder.Property(ci => ci.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(ci => ci.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(ci => ci.ProductImageUrl)
                .HasMaxLength(500);

        }
    }
}
