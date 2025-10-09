using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity,CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities,CancellationToken ct = default);
        void update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
    }
}
