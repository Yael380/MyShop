using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Services;
using Entities;
using AutoMapper;
using DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        IUserServices UserServices;
        IMapper mapper;
        private readonly ILogger <UsersController> logger;

        public UsersController(IUserServices userServices, IMapper mapper, ILogger<UsersController> logger)
        {
            UserServices = userServices;
            this.mapper = mapper;
            this.logger = logger;
        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> Get( int id)
        {
            User user = await UserServices.Get(id);
            GetUserDTO userDTO = mapper.Map<User, GetUserDTO>(user);
            if (userDTO != null)
                return Ok(userDTO);
            else
                return NoContent();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] PostUserDTO user)
        {
            User userDTO = mapper.Map<PostUserDTO,User>(user);
            User newUser =await UserServices.Post(userDTO);
            if (newUser == null)
            {
                return BadRequest();
            }
            GetUserDTO getUserDTO = mapper.Map<User, GetUserDTO>(newUser);
            return CreatedAtAction(nameof(Get), new { id = getUserDTO.Id }, getUserDTO);
        }
        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<GetUserDTO>> PostLogIn([FromQuery] string userName,string password)
        {
            User newUser = await UserServices.PostLogIn(userName, password);
            GetUserDTO userDTO = mapper.Map<User, GetUserDTO>(newUser);
            if (userDTO != null) {
                return Ok(userDTO);
            }
            else
                return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] PostUserDTO userInfo)
        {
            User user = mapper.Map<PostUserDTO, User>(userInfo);
            user.Id = id;
            User userUpdate =await UserServices.Put(id, user);
            if (userUpdate == null)
            {
                return BadRequest();
            }
            GetUserDTO userDTO = mapper.Map<User, GetUserDTO>(userUpdate);
            if (userDTO != null)
                return Ok(userDTO);
            else
                return NoContent();
        }
        [HttpGet]
        [Route("check")]
        public int CheckPassword([FromQuery] string password)
        {
            return UserServices.CheckPassword(password);
        }
    }
}
