//using MyShop;
using Resources;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Entities;
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
        public Task<User> Get(int id)
        {
            return UserResources.Get(id);
        }
        public Task<User> Post(User user)
        {
            int passwordScore = CheckPassword(user.Password);
            if (passwordScore < 3)
                return null;
            return UserResources.Post(user);
        }
        public Task<User> PostLogIn(string userName, string password)
        {
            return UserResources.PostLogIn(userName, password);
        }
        public Task<User> Put(int id, User userInfo)
        {
            int passwordScore = CheckPassword(userInfo.Password);
            if (passwordScore > 3)
                return UserResources.Put(id, userInfo);
            else 
                return null;
        }
        public int CheckPassword(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
