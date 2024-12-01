//using MyShop;
using Entities;

namespace Services
{
    public interface IUserServices
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string Get(int id);
        Task<User> Post(User user);
        Task<User> PostLogIn(string userName, string password);
        void Put(int id, User userInfo);
        int CheckPassword(string password);

    }
}