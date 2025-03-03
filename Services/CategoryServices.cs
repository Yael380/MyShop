using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        ICategoryRepository categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> Get()
        {
            return categoryRepository.Get();
        }
    }
}
