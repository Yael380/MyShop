using MyShop;
using Resources;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Zxcvbn;
namespace Services
{
    public class UserServices : IUserServices
    {
        IUserResources UserResources;
        public UserServices(IUserResources userResources)
        {
            this.UserResources = userResources;
        }
        public IEnumerable<string> Get()
        {
            return UserResources.Get();
        }
        public string Get(int id)
        {
            return UserResources.Get(id);
        }
        public User Post(User user)
        {
            return UserResources.Post(user);
        }
        public User PostLogIn(string userName, string password)
        {
            return UserResources.PostLogIn(userName, password);

        }
        public void Put(int id, User userInfo)
        {
            UserResources.Put(id, userInfo);
        }
        public void Delete(int id)
        {
        }
        public int CheckPassword(string password)
        {
            return  Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
