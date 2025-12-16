using ECommerceAPI.Application.Repositories.Addresses;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Address
{
    public class AddressesReadRepository : ReadRepository<Domain.Entities.Address>, IAddressesReadRepository
    {
        public AddressesReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
