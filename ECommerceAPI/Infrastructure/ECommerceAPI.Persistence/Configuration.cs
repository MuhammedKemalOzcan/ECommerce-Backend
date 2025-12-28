using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString { get
            {
               
                ConfigurationBuilder configurationManager = new();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API");
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json",optional:false);

                configurationManager.AddUserSecrets("3aa6699f-532e-4530-b4ae-8f1b650cacb5");

                configurationManager.AddEnvironmentVariables();

                return configurationManager.Build().GetConnectionString("PostgreSQL");
            } 
        }
    }
}
