using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.JwtGenerator;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ECommerceAPIDbContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ECommerceAPIDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<Iyzipay.Options>(sp =>
            {
                var options = new Iyzipay.Options();
                options.ApiKey = config["Iyzico:ApiKey"];
                options.SecretKey = config["Iyzico:SecretKey"];
                options.BaseUrl = config["Iyzico:BaseUrl"];
                return options;
            });

            services.AddScoped<ITokenHandler, JwtGenerator>();
            services.AddScoped<IIdentityService, IdentityServices>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IPaymentService, IyzicoPaymentService>();
        }

        //T => IStorage'dan türemiş bir class ise burayı çalıştır.
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}