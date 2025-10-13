using ECommerceAPI.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(
        IEnumerable<Expression<Func<T, object>>>? includes,
        Expression<Func<T, bool>>? predicate = null,
        bool asNoTracking = true,
        CancellationToken ct = default);

        Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct, bool asNoTracking = true);
        
        
    }
}
