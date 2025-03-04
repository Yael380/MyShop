using System.Reflection.Metadata;
using Entities;
using Microsoft.EntityFrameworkCore;
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

        // Unhappy Path Tests

        [Fact]
        public async Task GetUser_InvalidCredentials_ReturnsNull()
        {
            //Arrange
            var user = new User { FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.PostLogIn(user.UserName, "wrongpassword");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUser_ByInvalidId_ReturnsNull()
        {
            //Arrange
            var mockContext = new Mock<ApiManageContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());

            var userRepository = new UserRepository(mockContext.Object);

            //Act 
            var result = await userRepository.Get(-1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostUser_DuplicateUserName_ThrowsException()
        {
            //Arrange
            var user = new User { FirstName = "Rut", UserName = "test@test.test", Password = ")(*fkjl123" };

            var mockContext = new Mock<ApiManageContext>();
            mockContext.Setup(x => x.Users.AddAsync(user, default)).ThrowsAsync(new DbUpdateException());

            var userRepository = new UserRepository(mockContext.Object);

            //Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.Post(user));
        }
    }
}