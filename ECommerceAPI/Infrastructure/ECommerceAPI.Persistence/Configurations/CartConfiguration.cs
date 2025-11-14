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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts","dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired(false); // Kullanıcı girişi yapılmamışsa guest olarak sepete ekleme yapılabilir.

            builder.Property(x => x.SessionId).IsRequired(false).HasMaxLength(100);

            builder.Property(x => x.Status).HasConversion<int>(); //Enum tipini int olarak sakla.

            // Identity user tablosuna referans vermiyoruz
            // Sadece index ekliyoruz
            // Indexes
            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_Carts_UserId");

            builder.HasIndex(x => x.SessionId)
                .HasDatabaseName("IX_Carts_SessionId");

            builder.HasIndex(x => new { x.UserId, x.Status })
                .HasDatabaseName("IX_Carts_UserId_Status");

            builder.HasIndex(x => x.ExpiryDate)
                .HasDatabaseName("IX_Carts_ExpiryDate")
                .HasFilter("[Status] = 1"); // Sadece aktif sepetler

            builder.HasIndex(x => new { x.SessionId, x.Status })
                .HasDatabaseName("IX_Carts_SessionId_Status");

            // Computed columns ignore edilmeli
            builder.Ignore(x => x.TotalAmount);
            builder.Ignore(x => x.TotalItemCount);

            // Cart → CartItems(1:N), FK: CartItems.CartId
            builder.HasMany(x => x.CartItems)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
