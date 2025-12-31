using ECommerceAPI.Application.Abstractions.Data;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.CartItems;
using ECommerceAPI.Application.Repositories.Carts;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Repositories.Cart;
using ECommerceAPI.Persistence.Repositories.CartItem;
using ECommerceAPI.Persistence.Repositories.Carts;
using ECommerceAPI.Persistence.Repositories.File;
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<ICartsReadRepository, CartReadRepository>();
            services.AddScoped<ICartsWriteRepository, CartWriteRepository>();
            services.AddScoped<ICartItemReadRepository, CartItemReadRepository>();
            services.AddScoped<ICartItemWriteRepository, CartItemWriteRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEcommerceAPIDbContext>(provider =>
    provider.GetRequiredService<ECommerceAPIDbContext>());
        }
    }
}
