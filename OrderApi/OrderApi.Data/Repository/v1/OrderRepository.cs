using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Database;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Data.Repository.v1
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(OrderContext orderContext):base(orderContext)
        {

        }
        public async Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await orderContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return await orderContext.Orders.Where(o=>o.CustomerId==customerId).ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetPaidOrdersAsync(CancellationToken cancellationToken)
        {
            return await orderContext.Orders.Where(o => o.OrderState==2).ToListAsync(cancellationToken);
        }
    }
}
