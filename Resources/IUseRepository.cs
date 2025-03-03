//using MyShop;
using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> Get(int id);
        Task<User> Post(User user);
        Task<User> PostLogIn(string userName, string password);
        Task<User> Put(int id, User userInfo);
    }
}