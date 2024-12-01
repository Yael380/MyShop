//using MyShop;
using Entities;

namespace Resources
{
    public interface IUserResources
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string Get(int id);
        Task<User> Post(User user);
        Task<User> PostLogIn(string userName, string password);
        Task Put(int id, User userInfo);
    }
}