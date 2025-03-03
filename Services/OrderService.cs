using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        IProductRepository productRepository;
        private readonly ILogger<OrderRepository> logger;

        public OrderService(IOrderRepository _orderRepository, IProductRepository _productRepository, ILogger<OrderRepository> logger)
        {
            this.orderRepository = _orderRepository;
            this.productRepository = _productRepository;
            this.logger = logger;
        }

        public Task<Order> Get(int id)
        {
            return orderRepository.Get(id);
        }
        public async  Task<Order> Post(Order newOrder)
        {
            List<Product> products = await productRepository.Get(null,null,new int?[0],null);
            double ? sum = 0;
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
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            newOrder.Date = dateNow;
            return await orderRepository.Post(newOrder);
        }
    }
}
