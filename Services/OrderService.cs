using Entities;
using Resources;
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

        public OrderService(IOrderRepository _orderRepository)
        {
            this.orderRepository = _orderRepository;
        }

        public Task<Order> Get(int id)
        {
            return orderRepository.Get(id);
        }
        public Task<Order> Post(Order newOrder)
        {
            return orderRepository.Post(newOrder);
        }
    }
}
