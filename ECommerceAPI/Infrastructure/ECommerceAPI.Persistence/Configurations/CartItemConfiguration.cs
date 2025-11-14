using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasIndex(x => x.CartId)
               .HasDatabaseName("IX_CartItems_CartId");

            builder.HasIndex(x => x.ProductId)
                .HasDatabaseName("IX_CartItems_ProductId");

            // Cart → CartItems(1:N), FK: CartItems.ProductId
            builder.HasOne(x => x.Product)
           .WithMany()
           .HasForeignKey(x => x.ProductId)
           .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint: Aynı üründen sepette sadece 1 kayıt
            builder.HasIndex(x => new { x.CartId, x.ProductId })
                .IsUnique();
        }
    }
}
