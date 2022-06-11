using API.DataModels;
using API.Models.Orders;
using API.Projections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.IRepositories
{
    public interface IOrderRepository: IGenericRepository  
    {
        Task<OrderProjection> Create(Order order);
        Task<OrderProjection> Update(Order order);

        Task<bool> Delete(int id);

        Task<IEnumerable<OrderProjection>> GetAll(OrderQueryModel orderQuery);
        Task<IEnumerable<OrderProjection>> GetAll();
    }
}
