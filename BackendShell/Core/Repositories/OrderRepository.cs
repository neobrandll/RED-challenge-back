using API.Core.IRepositories;
using API.DataModels;
using API.Models;
using API.Projections;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {

        public OrderRepository(
            ApplicationDbContext context, ILogger logger): base(context,logger)
        {

        }
      
       public async Task<OrderProjection> Create(Order entity)
        {
            try
            {
                _context.Orders.Add(entity);
                await _context.SaveChangesAsync();
                return new OrderProjection(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Create method error", typeof(OrderRepository));
                return null;
            }
        }

       public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _context.Orders.FindAsync(id);

                if (result == null)
                    return false;

                _context.Orders.Remove(result);
              await  _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(OrderRepository));
                return false;         
                    }
        }



        public async Task<IEnumerable<OrderProjection>> GetAll()
        {
            try
            {
                var result = from order in _context.Orders
                             select new OrderProjection(order);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get All method error", typeof(OrderRepository));
                return new List<OrderProjection>();
            }
        }
        public async Task<IEnumerable<OrderProjection>> GetAll(OrderQuery orderQuery)
        {
            try
            {
                var result = from order in _context.Orders
                             where order.OrderType == orderQuery.orderType
                             where order.CustomerName.Contains(orderQuery.customerName)
                             select new OrderProjection(order);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get All method error", typeof(OrderRepository));
                return new List<OrderProjection>();
            }
        }

       
      
        public async Task<OrderProjection> Update(Order order)
        {
            try
            {
                var result = await _context.Orders.FindAsync(order.OrderId);
                if (result == null) return null;
                result = order;

                 _context.SaveChanges();

                return new OrderProjection(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(OrderRepository));
                return null;
            }
        }

          }
}
