//using MyShop;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Resources 
{
    public class UserResources : IUserResources
    {
        ApiManageContext context;
        public UserResources(ApiManageContext apiManageContext)
        {
            context = apiManageContext;
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public string Get(int id)
        {
            return "value";
        }
        public async Task<User> Post(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<User> PostLogIn(string userName, string password)
        {
            User userFind = await context.Users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
            return userFind;
        }
        public async Task Put(int id, User userInfo)
        {
            context.Users.Update(userInfo);
            await context.SaveChangesAsync();
            return;
        }
        public void Delete(int id)
        {
        }
    }
}
