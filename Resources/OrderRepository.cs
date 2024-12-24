using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class OrderRepository : IOrderRepository
    {
        ApiManageContext context;
        public OrderRepository(ApiManageContext context)
        {
            this.context = context;
        }

        public async Task<Order> Get(int id)
        {
            return await context.Orders.Include(o=> o.OrderItems).FirstOrDefaultAsync(order => order.Id==id);
        }
        public async Task<Order> Post(Order newOrder)
        {
            await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
            return newOrder;
        }

    }
}
