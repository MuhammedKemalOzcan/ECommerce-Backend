using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPI.SignalR.HubService
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub,IOrderClient> _hubContext;

        public OrderHubService(IHubContext<OrderHub, IOrderClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NewOrderMessageAsync(string message)
        {
            await _hubContext.Clients.Group("Admins").ReceiveMessage(message);
        }
    }
}