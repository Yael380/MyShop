using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> Get(int id);
        Task<Order> Post(Order newOrder);
    }
}