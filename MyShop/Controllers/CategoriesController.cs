﻿//using Entities;
//using Microsoft.AspNetCore.Mvc;
//using Services;
//using System.Collections.Generic;
//using DTO;
//using AutoMapper;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace MyShop.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoriesController : ControllerBase
//    {
//        ICategoryServices categoryServices;
//        IMapper mapper;
//        public CategoriesController(ICategoryServices categoryServices,IMapper mapper)
//        {
//            this.categoryServices = categoryServices;
//            this.mapper = mapper;
//        }

//        // GET: api/<CategoriesController>
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> Get()
//        {
//            IEnumerable<Category> categories = await categoryServices.Get();
//            IEnumerable<GetCategoryDTO> categoriesDTO = mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDTO>>(categories);
//            if (categoriesDTO != null)
//                return Ok(categoriesDTO);
//            else
//                return NoContent();
//        }
//    }
//}
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly string cacheKey = "categoriesCacheKey";

        public CategoriesController(ICategoryServices categoryServices, IMapper mapper, IMemoryCache memoryCache)
        {
            this.categoryServices = categoryServices;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        //[ResponseCache(Duration = 60)] // Caches the response for 60 seconds
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> Get()
        {
            if (!memoryCache.TryGetValue(cacheKey, out IEnumerable<GetCategoryDTO> categoriesDTO))
            {
                IEnumerable<Category> categories = await categoryServices.Get();
                categoriesDTO = mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDTO>>(categories);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    //SlidingExpiration = TimeSpan.FromSeconds(30)
                };

                memoryCache.Set(cacheKey, categoriesDTO, cacheEntryOptions);
            }

            if (categoriesDTO != null)
                return Ok(categoriesDTO);
            else
                return NoContent();
        }
    }
}