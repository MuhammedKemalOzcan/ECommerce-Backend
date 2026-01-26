using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Products;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceAPIDbContext _context;

        public ProductRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public Task<Product?> GetByIdAsync(ProductId productId)
        {
            return _context.Products
                .Include(p => p.ProductBoxes)
                .Include(p => p.ProductGalleries)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product?> GetByIdOrThrowAsync(ProductId productId)
        {
            var product = await GetByIdAsync(productId);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {productId.Value} not found.");
            }

            return product;
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<ProductId> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
