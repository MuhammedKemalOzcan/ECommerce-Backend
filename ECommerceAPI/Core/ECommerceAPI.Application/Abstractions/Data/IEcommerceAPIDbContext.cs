using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerceAPI.Application.Abstractions.Data
{
    public interface IEcommerceAPIDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBox> ProductBoxes { get; set; }
        public DbSet<ProductGallery> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Customer> Customers { get; set; }
        DatabaseFacade Database { get; }

    }
}
