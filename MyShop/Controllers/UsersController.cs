using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Services;
using Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserServices UserServices;
        public UsersController(IUserServices userServices)
        {
            UserServices = userServices;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return UserServices.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return UserServices.Get(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            User newUser =await UserServices.Post(user);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }
        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> PostLogIn([FromQuery] string userName,string password)
        {
            User newUser = await UserServices.PostLogIn(userName, password);
            if(newUser!=null)
                return Ok(newUser);
            else
                return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userInfo)
        {
            UserServices.Put(id, userInfo);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        // GET: api/<UsersController>
        [HttpPost]
        [Route("check")]
        public int CheckPassword([FromQuery] string password)
        {
            return UserServices.CheckPassword(password);
        }
    }
}
