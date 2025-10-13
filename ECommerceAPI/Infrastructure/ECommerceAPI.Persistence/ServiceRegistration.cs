using ECommerceAPI.Application.Repositories.ProductBoxes;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.ProductBoxes;
using ECommerceAPI.Persistence.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceAPIDbContext>(opt =>
            {
                opt.UseNpgsql(Configuration.ConnectionString);
            });

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductBoxReadRepository, ProductBoxReadRepository>();
            services.AddScoped<IProductBoxWriteRepository, ProductBoxWriteRepository>();
        }
    }
}
