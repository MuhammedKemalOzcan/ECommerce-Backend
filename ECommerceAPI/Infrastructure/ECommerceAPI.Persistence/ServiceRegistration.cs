using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.Addresses;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.ProductGallery;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Address;
using ECommerceAPI.Persistence.Repositories.Cart;
using ECommerceAPI.Persistence.Repositories.CartItem;
using ECommerceAPI.Persistence.Repositories.Carts;
using ECommerceAPI.Persistence.Repositories.Customer;
using ECommerceAPI.Persistence.Repositories.File;
using ECommerceAPI.Persistence.Repositories.ProductBoxes;
using ECommerceAPI.Persistence.Repositories.ProductGallery;
using ECommerceAPI.Persistence.Repositories.Products;
using ECommerceAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceAPIDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            });

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductBoxReadRepository, ProductBoxReadRepository>();
            services.AddScoped<IProductBoxWriteRepository, ProductBoxWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductGalleryReadRepository, ProductGalleryReadRepository>();
            services.AddScoped<IProductGalleryWriteRepository, ProductGalleryWriteRepository>();
            services.AddScoped<ICartsReadRepository, CartReadRepository>();
            services.AddScoped<ICartsWriteRepository, CartWriteRepository>();
            services.AddScoped<ICartItemReadRepository, CartItemReadRepository>();
            services.AddScoped<ICartItemWriteRepository, CartItemWriteRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IAddressesReadRepository, AddressesReadRepository>();
            services.AddScoped<IAddressesWriteRepository, AddressesWriteRepository>();

        }
    }
}
