namespace ECommerceAPI.Application.Abstractions.Hubs
{
    public interface IOrderClient
    {
        Task ReceiveMessage(string message);
    }
}
