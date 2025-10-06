using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString { get
            {
                ConfigurationManager config = new();
                config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
                config.AddJsonFile("appsettings.json",optional:false);

                return config.GetConnectionString("PostgreSQL");
            } 
        }
    }
}
