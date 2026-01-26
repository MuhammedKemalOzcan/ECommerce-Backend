using ECommerceAPI.Domain.Entities.Cart;
using ECommerceAPI.Domain.Entities.Customer;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerceAPI.Application.Abstractions.Data
{
    public interface IEcommerceAPIDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        DatabaseFacade Database { get; }

    }
}
