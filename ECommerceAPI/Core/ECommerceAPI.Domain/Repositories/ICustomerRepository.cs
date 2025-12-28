using ECommerceAPI.Domain.Entities.Customer;

namespace ECommerceAPI.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByUserIdAsync(Guid appUserId);

        void Add(Customer customer);
    }
}
