namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();
    }
}
