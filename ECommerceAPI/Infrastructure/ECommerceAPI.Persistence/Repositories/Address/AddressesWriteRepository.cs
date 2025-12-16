using ECommerceAPI.Application.Repositories.Addresses;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Address
{
    public class AddressesWriteRepository : WriteRepository<Domain.Entities.Address>, IAddressesWriteRepository
    {
        public AddressesWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
