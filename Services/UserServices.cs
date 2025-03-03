//using MyShop;
using Repository;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Entities;
using Zxcvbn;

namespace Services
{
    public class UserServices : IUserServices
    {
        IUserRepository UserRepository;
        public UserServices(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public Task<User> Get(int id)
        {
            return UserRepository.Get(id);
        }
        public Task<User> Post(User user)
        {
            int passwordScore = CheckPassword(user.Password);
            if (passwordScore < 3)
                return null;
            return UserRepository.Post(user);
        }
        public Task<User> PostLogIn(string userName, string password)
        {
            return UserRepository.PostLogIn(userName, password);
        }
        public Task<User> Put(int id, User userInfo)
        {
            int passwordScore = CheckPassword(userInfo.Password);
            if (passwordScore >= 3)
                return UserRepository.Put(id, userInfo);
            else 
                return null;
        }
        public int CheckPassword(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
