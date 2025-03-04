using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Services.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<OrderService>>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _productRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Post_ValidOrder_ReturnsOrder()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Price = 100 },
                new Product { Id = 2, Price = 50 }
            };
            _productRepositoryMock.Setup(repo => repo.Get(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<int?[]>(), It.IsAny<object>()))
                .ReturnsAsync(products);

            var newOrder = new Order
            {
                UserId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                },
                Sum = 250 // סכום נכון לבדיקה
            };

            _orderRepositoryMock.Setup(repo => repo.Post(newOrder)).ReturnsAsync(newOrder);

            // Act
            var result = await _orderService.Post(newOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newOrder.UserId, result.UserId);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.Date);
            Assert.Equal(250, result.Sum);
        }

        [Fact]
        public async Task Post_InvalidSum_LogsCritical()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Price = 100 },
                new Product { Id = 2, Price = 50 }
            };
            _productRepositoryMock.Setup(repo => repo.Get(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<int?[]>(), It.IsAny<object>()))
                .ReturnsAsync(products);

            var newOrder = new Order
            {
                UserId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                },
                Sum = 300 // סכום לא נכון לבדיקה
            };

            _orderRepositoryMock.Setup(repo => repo.Post(newOrder)).ReturnsAsync(newOrder);

            // Act
            var result = await _orderService.Post(newOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newOrder.UserId, result.UserId);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.Date);
            Assert.Equal(250, result.Sum); // הסכום המתוקן

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Critical,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("User id 1 is trying to hack your order amount")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }

    // Mock interfaces for the repositories
    public interface IOrderRepository
    {
        Task<Order> Post(Order newOrder);
    }

    public interface IProductRepository
    {
        Task<List<Product>> Get(object arg1, object arg2, int?[] arg3, object arg4);
    }

    // Mock classes for the entities
    public class Order
    {
        public int UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double? Sum { get; set; }
        public DateOnly Date { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
    }

    // Order service class
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly ILogger<OrderService> logger;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<OrderService> logger)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task<Order> Post(Order newOrder)
        {
            List<Product> products = await productRepository.Get(null, null, new int?[0], null);
            checkSum(products, newOrder);
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            newOrder.Date = dateNow;
            return await orderRepository.Post(newOrder);
        }

        private void checkSum(List<Product> products, Order newOrder)
        {
            double? sum = 0;
            foreach (var item in newOrder.OrderItems)
            {
                Product? p = products.FirstOrDefault(p => p.Id == item.ProductId);
                sum += p?.Price * item.Quantity;
            }
            if (newOrder.Sum != sum)
            {
                newOrder.Sum = sum;
                logger.LogCritical($"User id {newOrder.UserId} is trying to hack your order amount ");
            }
        }
    }
}