using MyShop;

namespace Services
{
    public interface IUserServices
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string Get(int id);
        User Post(User user);
        User PostLogIn(string userName, string password);
        void Put(int id, User userInfo);
        int CheckPassword(string password);

    }
}