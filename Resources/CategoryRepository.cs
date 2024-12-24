using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class CategoryRepository : ICategoryRepository
    {
        ApiManageContext context;
        public CategoryRepository(ApiManageContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
