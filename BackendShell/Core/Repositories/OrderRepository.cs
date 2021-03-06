using API.Core.IRepositories;
using API.DataModels;
using API.Projections;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Models.Orders;

namespace API.Core.Repositories
{
    public class OrderRepository : GenericRepository, IOrderRepository
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

       public async Task<bool> Delete(List<int> ids)
        {
            try
            {
                 _context.Orders.RemoveRange(_context.Orders.Where(x => ids.Contains(x.OrderId) ));

                
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
        public async Task<IEnumerable<OrderProjection>> GetAll(OrderQueryModel orderQuery)
        {
            try
            {
                IQueryable<Order> result = _context.Orders;
                if (orderQuery.orderType != null)
                {
                    result = result.Where(x => x.OrderType == orderQuery.orderType);
                }
                if (orderQuery.customerName != null)
                {
                    result = result.Where(x => x.CustomerName.Contains(orderQuery.customerName));
                }
                if (orderQuery.orderId != null)
                {
                    result = result.Where(x => x.OrderId == orderQuery.orderId);
                }
                var orders = result.Select(x => new OrderProjection(x));
                var list = await orders.ToListAsync();
                return list.AsEnumerable<OrderProjection>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get All method error", typeof(OrderRepository));
                return null;
            }
        }

        public async Task<Order> GetById(int id)
        {
            var result = await _context.Orders.FindAsync(id);
            if (result == null) return null;
            return result;
        }

        public async Task<OrderProjection> Update(Order order)
        {
            try
            {
                var result = await _context.Orders.FindAsync(order.OrderId);
                if (result == null) return null;
                result.CreatedDate = order.CreatedDate;
                result.CustomerName = order.CustomerName;
                result.OrderType = order.OrderType;
                result.CreatedByUserName = order.CreatedByUserName;

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
