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
        Task<OrderProjection> IOrderRepository.Create(Order order)
        {
            throw new NotImplementedException();
        }

        Task<IOrderRepository> IGenericRepository<IOrderRepository>.Create(IOrderRepository entity)
        {
            throw new NotImplementedException();
        }

       public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = _context.Orders.Find(id);

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

       
      
        public override async Task<OrderProjection> Update(Order order)
        {
            try
            {
                var result = await _context.Orders.FindAsync(order.OrderId);
                if (result == null)
                    return null;
                result = order;

                await _context.SaveChangesAsync();

                return new OrderProjection(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get All method error", typeof(OrderRepository));
                return null;
            }
        }

        Task<IOrderRepository> IGenericRepository<IOrderRepository>.Update(IOrderRepository entity)
        {
            throw new NotImplementedException();
        }
    }
}
