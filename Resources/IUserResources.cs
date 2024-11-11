using MyShop;

namespace Resources
{
    public interface IUserResources
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string Get(int id);
        User Post(User user);
        User PostLogIn(string userName, string password);
        void Put(int id, User userInfo);
    }
}