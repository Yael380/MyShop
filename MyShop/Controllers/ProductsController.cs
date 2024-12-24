using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductServices productServices;
        IMapper mapper;
        public ProductsController(IProductServices _productServices, IMapper mapper)
        {
            this.productServices = _productServices;
            this.mapper=mapper;
    }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> Get()
        {
            IEnumerable<Product> products = await productServices.Get();
            IEnumerable<GetProductDTO> productsDTO = mapper.Map<IEnumerable<Product>, IEnumerable<GetProductDTO>>(products);

            if (productsDTO != null)
                return Ok(productsDTO);
            else
                return NoContent();
        }
        

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> Get(int id)
        {
            Product product = await productServices.Get(id);
            GetProductDTO productDTO = mapper.Map<Product, GetProductDTO>(product);
            if (productDTO != null)
                return Ok(productDTO);
            else
                return NoContent();
        }

    }
}
