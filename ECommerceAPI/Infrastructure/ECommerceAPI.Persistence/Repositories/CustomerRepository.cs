using ECommerceAPI.Domain.Entities.Customer;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories
{
    internal sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ECommerceAPIDbContext _context;

        public CustomerRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<Customer?> GetById(Guid customerId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == new CustomerId(customerId));
        }

        public async Task<Customer?> GetByUserIdAsync(Guid appUserId)
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .SingleOrDefaultAsync(c => c.AppUserId == appUserId);
        }

    }
}
