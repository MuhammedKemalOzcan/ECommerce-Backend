using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        public ECommerceAPIDbContext CreateDbContext(string[] args)
        {

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ECommerceAPIDbContext>();

            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);


            return new ECommerceAPIDbContext(dbContextOptionsBuilder.Options);

        }
    }
}
