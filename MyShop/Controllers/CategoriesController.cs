using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using DTO;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryServices categoryServices;
        IMapper mapper;
        public CategoriesController(ICategoryServices categoryServices,IMapper mapper)
        {
            this.categoryServices = categoryServices;
            this.mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> Get()
        {
            IEnumerable<Category> categories = await categoryServices.Get();
            IEnumerable<GetCategoryDTO> categoriesDTO = mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDTO>>(categories);
            if (categoriesDTO != null)
                return Ok(categoriesDTO);
            else
                return NoContent();
        }
    }
}
