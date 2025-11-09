using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        void Attach(T entity);
    }
}
