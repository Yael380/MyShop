//using Entities;
//using Repository;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace TestProject1;

//public class UserRepositoryIntegrationTesting
//{
//    private readonly DbContextOptions<ApiManageContext> _contextOptions;

//    public UserRepositoryIntegrationTesting()
//    {
//        _contextOptions = new DbContextOptionsBuilder<ApiManageContext>()
//            .UseSqlServer("Server = SRV2\\PUPILS; Initial Catalog = API_manage_Test3; Integrated Security = True;TrustServerCertificate=True")
//            .Options;

//        SeedDatabase();
//    }

//    private void SeedDatabase()
//    {
//        using var context = new ApiManageContext(_contextOptions);
//        context.Database.EnsureDeleted();
//        context.Database.EnsureCreated();

//        var user = new User
//        {
//            FirstName = "tamar",
//            LastName = "tamar",
//            UserName = "test@gmail.com",
//            Password = ",nr2155572"
//        };

//        context.Users.Add(user);
//        context.SaveChanges();
//    }

//    [Fact]
//    public async Task GetUser_ValidCredentials_ReturnsUser()
//    {
//        // Arrange
//        using var context = new ApiManageContext(_contextOptions);
//        var userRepository = new UserRepository(context);

//        // Act
//        var result = await userRepository.PostLogIn("test@gmail.com", ",nr2155572");

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("tamar", result.FirstName);
//    }

//    [Fact]
//    public async Task GetUserById_ExistingId_ReturnsUser()
//    {
//        // Arrange
//        using var context = new ApiManageContext(_contextOptions);
//        var userRepository = new UserRepository(context);

//        // Act
//        var result = await userRepository.Get(1);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("tamar", result.FirstName);
//    }

//    [Fact]
//    public async Task PostUser_ValidUser_ReturnsUser()
//    {
//        // Arrange
//        var newUser = new User
//        {
//            FirstName = "newUser",
//            UserName = "newuser@gmail.com",
//            Password = "newpassword"
//        };

//        using var context = new ApiManageContext(_contextOptions);
//        var userRepository = new UserRepository(context);

//        // Act
//        var result = await userRepository.Post(newUser);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("newUser", result.FirstName);
//    }

//    [Fact]
//    public async Task PutUser_ValidUser_ReturnsUpdatedUser()
//    {
//        // Arrange
//        using var context = new ApiManageContext(_contextOptions);
//        var userRepository = new UserRepository(context);

//        var updatedUser = new User
//        {
//            Id = 1,
//            FirstName = "updatedName",
//            UserName = "test@gmail.com",
//            Password = ",nr2155572"
//        };

//        // Act
//        var result = await userRepository.Put(updatedUser.Id, updatedUser);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("updatedName", result.FirstName);
//    }
//}
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1;

public class UserRepositoryIntegrationTesting
{
    private readonly DbContextOptions<ApiManageContext> _contextOptions;

    public UserRepositoryIntegrationTesting()
    {
        _contextOptions = new DbContextOptionsBuilder<ApiManageContext>()
            .UseSqlServer("Server=SRV2\\PUPILS;Initial Catalog=API_manage_Test3;Integrated Security=True;TrustServerCertificate=True")
            .Options;

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        using var context = new ApiManageContext(_contextOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var user = new User
        {
            FirstName = "tamar",
            LastName = "tamar",
            UserName = "test@gmail.com",
            Password = ",nr2155572"
        };

        context.Users.Add(user);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetUser_ValidCredentials_ReturnsUser()
    {
        // Arrange
        using var context = new ApiManageContext(_contextOptions);
        var userRepository = new UserRepository(context);

        // Act
        var result = await userRepository.PostLogIn("test@gmail.com", ",nr2155572");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("tamar", result.FirstName);
    }

    [Fact]
    public async Task GetUserById_ExistingId_ReturnsUser()
    {
        // Arrange
        using var context = new ApiManageContext(_contextOptions);
        var userRepository = new UserRepository(context);

        // Act
        var result = await userRepository.Get(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("tamar", result.FirstName);
    }

    [Fact]
    public async Task PostUser_ValidUser_ReturnsUser()
    {
        // Arrange
        var newUser = new User
        {
            FirstName = "newUser",
            UserName = "newuser@gmail.com",
            Password = "newpassword"
        };

        using var context = new ApiManageContext(_contextOptions);
        var userRepository = new UserRepository(context);

        // Act
        var result = await userRepository.Post(newUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newUser", result.FirstName);
    }

    [Fact]
    public async Task PutUser_ValidUser_ReturnsUpdatedUser()
    {
        // Arrange
        using var context = new ApiManageContext(_contextOptions);
        var userRepository = new UserRepository(context);

        var updatedUser = new User
        {
            Id = 1,
            FirstName = "updatedName",
            UserName = "test@gmail.com",
            Password = ",nr2155572"
        };

        // Act
        var result = await userRepository.Put(updatedUser.Id, updatedUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("updatedName", result.FirstName);
    }
}