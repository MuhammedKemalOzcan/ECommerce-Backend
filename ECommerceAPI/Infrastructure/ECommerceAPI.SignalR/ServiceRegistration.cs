using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.HubService;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRService(this IServiceCollection collection)
        {
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddSignalR();
        }
    }
}