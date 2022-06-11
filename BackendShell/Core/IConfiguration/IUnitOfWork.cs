using API.Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IAuthRepository Auth { get; }
        UserManager<IdentityUser> UserManager { get; }

        Task CompleteAsync();
    }
}
