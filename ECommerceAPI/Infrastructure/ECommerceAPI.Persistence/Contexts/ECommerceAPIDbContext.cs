using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ECommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ECommerceAPIDbContext(DbContextOptions<ECommerceAPIDbContext> options) : base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBox> ProductBoxes { get; set; }
        public DbSet<ProductGallery> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(b =>
            {
                b.Property(p => p.Name).IsRequired();
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            builder.Entity<ProductBox>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Product)
                .WithMany(p => p.ProductBoxes)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ProductGallery>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Product)
                .WithMany(p => p.ProductGalleries)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in entities)
            {
                _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);

        }

    }
}
