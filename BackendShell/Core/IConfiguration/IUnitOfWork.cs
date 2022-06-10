using API.Core.IRepositories;
using System.Threading.Tasks;

namespace API.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IAuthRepository Auth { get; }

        Task CompleteAsync();
    }
}
