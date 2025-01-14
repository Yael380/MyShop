using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService orderService;
        IMapper mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTO>> Get(int id)
        {
            Order order = await orderService.Get(id);
            GetOrderDTO orderDTO = mapper.Map<Order, GetOrderDTO>(order);
            if (orderDTO != null)
                return Ok(orderDTO);
            else
                return NoContent();
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostOrderDTO orderDTO)
        {
            Order order = mapper.Map<PostOrderDTO, Order>(orderDTO);
            Order newOrder = await orderService.Post(order);
            GetOrderDTO orderdto = mapper.Map<Order,GetOrderDTO>(newOrder);
            return CreatedAtAction(nameof(Get), new { id = orderdto.Id }, orderdto);
        }

    }
}
