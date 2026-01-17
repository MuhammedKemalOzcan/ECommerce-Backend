using ECommerceAPI.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories.Carts
{
    public interface ICartsReadRepository : IReadRepository<Cart>
    {
        Task<Cart?> GetActiveCartWithDetailsAsync(Expression<Func<Cart, bool>> predicate, CancellationToken ct = default);
    }
}
