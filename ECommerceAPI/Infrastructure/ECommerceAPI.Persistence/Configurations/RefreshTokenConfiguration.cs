using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Persistence.Configurations
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Token).HasMaxLength(200);

            builder.HasIndex(r => r.Token).IsUnique();

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId);
        }
    }
}
