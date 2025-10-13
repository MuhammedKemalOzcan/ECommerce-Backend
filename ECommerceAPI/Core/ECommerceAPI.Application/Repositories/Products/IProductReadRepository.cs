using ECommerceAPI.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories.Products
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
        Task<Product> GetByIdAsync(Guid id, IEnumerable<Expression<Func<Product, object>>>? includes = null, bool asNoTracking = true);
    }
}
