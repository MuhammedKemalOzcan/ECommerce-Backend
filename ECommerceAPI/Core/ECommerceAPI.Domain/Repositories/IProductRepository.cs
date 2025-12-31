using ECommerceAPI.Domain.Entities.Products;

namespace ECommerceAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(ProductId productId);
        Task<Product?> GetByIdOrThrowAsync(ProductId productId);
        void Add(Product product);
        void Remove(Product product);
    }
}
