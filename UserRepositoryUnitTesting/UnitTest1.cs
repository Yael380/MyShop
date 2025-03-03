//using System.Reflection.Metadata;
//using Entities;
//using Moq;
//using Moq.EntityFrameworkCore;
//using Repository;

//namespace UserRepositoryUnitTesting
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public async Task GetUser_ValidCredentials_ReturnsUser()
//        {
//            //Arrange
//            var user = new User { FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

//            var mockContext = new Mock<ApiManageContext>();
//            var users = new List<User>() { user };
//            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

//            var userRepository = new UserRepository(mockContext.Object);

//            //Act 
//            var result = await userRepository.PostLogIn(user.UserName, user.Password);

//            //Assert
//            Assert.Equal(user, result);
//        }

//    }
//}

using System.Reflection.Metadata;
using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Xunit;

namespace UserRepositoryUnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            //Arrange
            var user = new User { FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.PostLogIn(user.UserName, user.Password);

            //Assert
            Assert.Equal(user, result);
        }

        // כתיבת בדיקות נוספות לכל הפונקציות

        [Fact]
        public async Task GetUser_ById_ReturnsUser()
        {
            //Arrange
            var user = new User { Id = 1, FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.Get(user.Id);

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task PostUser_CreatesNewUser_ReturnsUser()
        {
            //Arrange
            var user = new User { FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            mockContext.Setup(x => x.Users.AddAsync(user, default)).ReturnsAsync(new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User>(null));
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.Post(user);

            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task PutUser_UpdatesUser_ReturnsUpdatedUser()
        {
            //Arrange
            var user = new User { Id = 1, FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            mockContext.Setup(x => x.Users.Update(user));
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.Put(user.Id, user);

            //Assert
            Assert.Equal(user, result);
        }

    }
}
