
using Entities;

namespace Services
{
    public interface IProductServices
    {
        Task<Product> Get(int id);
        Task<List<Product>> Get(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc);
    }
}