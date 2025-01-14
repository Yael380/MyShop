using Entities;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices : IProductServices
    {
        IProductRepository productRepository;
        public ProductServices(IProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }
        public Task<List<Product>> Get(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc)
        {
            return productRepository.Get(minPrice, maxPrice, categoryIds, desc);
        }
        public Task<Product> Get(int id)
        {
            return productRepository.Get(id);
        }

    }
}
