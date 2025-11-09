using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;
        public WriteRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        IQueryable<T> IWriteRepository<T>.Table => Table;

        //Sonucu umursamıyoruz bu yüzden return etmemize gerek yok await ile bekle ve bitir.
        //AsTask() → Task<EntityEntry<T>> döndürür.
        public async Task AddAsync(T entity,CancellationToken ct = default)
            => await Table.AddAsync(entity,ct).AsTask();

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
            => Table.AddRangeAsync(entities, ct);

        public void Attach(T entity)
         => Table.Attach(entity);

        public void Remove(T entity)
            => Table.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities)
            => Table.RemoveRange(entities);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Update(T entity)
            => Table.Update(entity);

    }
}
