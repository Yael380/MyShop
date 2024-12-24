using Entities;

namespace Resources
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> Get();
    }
}