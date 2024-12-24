//using MyShop;
using Entities;

namespace Services
{
    public interface IUserServices
    {
        Task<User> Get(int id);
        Task<User> Post(User user);
        Task<User> PostLogIn(string userName, string password);
        Task<User> Put(int id, User userInfo);
        int CheckPassword(string password);

    }
}