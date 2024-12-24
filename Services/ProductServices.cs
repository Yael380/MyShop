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
        public Task<IEnumerable<Product>> Get()
        {
            return productRepository.Get();
        }
        public Task<Product> Get(int id)
        {
            return productRepository.Get(id);
        }

    }
}
