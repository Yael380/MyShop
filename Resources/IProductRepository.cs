
using Entities;

namespace Resources
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<List<Product>> Get(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc);
    }
}