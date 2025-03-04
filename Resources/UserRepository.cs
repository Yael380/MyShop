//using MyShop;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
namespace Repository
{
    public class UserRepository : IUserRepository
    {
        ApiManageContext context;
        public UserRepository(ApiManageContext apiManageContext)
        {
            context = apiManageContext;
        }
        public async Task<User> Get(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> Post(User user)
        {
            var res = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            //return user;
            return res.Entity;
        }
        public async Task<User> PostLogIn(string userName, string password)
        {
            User userFind = await context.Users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
            return userFind;
        }
        public async Task<User> Put(int id, User userInfo)
        {
            userInfo.Id = id;
            context.Users.Update(userInfo);
            await context.SaveChangesAsync();
            return userInfo;
        }
    }
}