using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories.Customers
{
    public interface ICustomerReadRepository : IReadRepository<Domain.Entities.Customer>
    {
        Task<Domain.Entities.Customer> GetByUserIdAsync(Guid userId, bool AsNoTracking = true, CancellationToken ct = default);
    }
}
