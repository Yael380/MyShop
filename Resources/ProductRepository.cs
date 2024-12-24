using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class ProductRepository : IProductRepository
    {
        ApiManageContext context;
        public ProductRepository(ApiManageContext apiManageContext)
        {
            context = apiManageContext;
        }
        public async Task<IEnumerable<Product>> Get()
        {
           return await context.Products.Include(p => p.Category).ToListAsync();
        }
        public async Task<Product> Get(int id)
        {
            return await context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
        }

    }
}
