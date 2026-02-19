namespace ECommerceAPI.Application.Abstractions.Hubs
{
    public interface IOrderHubService
    {
        Task NewOrderMessageAsync(string message);
    }
}