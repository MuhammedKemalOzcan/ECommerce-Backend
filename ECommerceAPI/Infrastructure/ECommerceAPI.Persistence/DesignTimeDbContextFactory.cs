using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        public ECommerceAPIDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseNpgsql(Configuration.ConnectionString);
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../../ECommerceAPI.API");

            return new ECommerceAPIDbContext(optionsBuilder.Options);

        }
    }
}
