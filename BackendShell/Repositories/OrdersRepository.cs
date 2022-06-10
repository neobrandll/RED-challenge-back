using API.Interfaces;
using API.Models;
using API.Projections;
using System.Threading.Tasks;
using System.Linq;
using API.DataModels;

namespace API.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly ApplicationDbContext _context;

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<OrderProjection> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new OrderProjection(order);
        }

        public Task DeleteOrder(int id)
        {
            throw new System.NotImplementedException();
        }

        public OrderProjection[] GetOrders(OrderQuery orderQuery)
        {
            var result = from order in _context.Orders
                         where order.OrderType == orderQuery.orderType
                         where order.CustomerName.Contains(orderQuery.customerName)
                         select new OrderProjection(order);
            return result.ToArray();
        }
        public OrderProjection[] GetOrders()
        {
            var result = from order in _context.Orders
                         select new OrderProjection(order);
            return result.ToArray();
        }

        public async Task<OrderProjection> UpdateOrder(Order order)
        {
            var result = await _context.Orders.FindAsync(order.OrderId);
            if (result == null)
                return null;
            result = order;

           await _context.SaveChangesAsync();

            return new OrderProjection(order);
        }
    }
    
}
