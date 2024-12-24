//using MyShop;
using Entities;

namespace Resources
{
    public interface IUserResources
    {
        Task<User> Get(int id);
        Task<User> Post(User user);
        Task<User> PostLogIn(string userName, string password);
        Task<User> Put(int id, User userInfo);
    }
}