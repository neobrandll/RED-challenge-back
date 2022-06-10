using API.DataModels;
using API.Models;
using API.Projections;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IOrdersService
    {
        OrderProjection[] GetOrders(OrderQuery orderQuery);
        Task<OrderProjection> CreateOrder(Order order);
        Task<OrderProjection> UpdateOrder(Order order);

        Task DeleteOrder(int id);

    }
}
