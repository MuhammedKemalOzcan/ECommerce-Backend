using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories.Carts
{
    public interface ICartsReadRepository : IReadRepository<Cart>
    {
        Task<Cart?> GetActiveCartWithDetailsAsync(Expression<Func<Cart, bool>> predicate, CancellationToken ct = default);
    }
}
