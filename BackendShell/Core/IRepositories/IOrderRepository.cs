using API.DataModels;
using API.Models;
using API.Projections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.IRepositories
{
    public interface IOrderRepository: IGenericRepository<IOrderRepository>
    {
        Task<OrderProjection> Create(Order order);
        Task<OrderProjection> Update(Order order);

        Task<IEnumerable<OrderProjection>> GetAll(OrderQuery orderQuery);
        Task<IEnumerable<OrderProjection>> GetAll();
    }
}
