using API.Models;
using API.Models.Auth;
using API.Projections;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserProjection> Login(LoginModel loginBody);
        Task<IdentityResult> Register(RegisterModel registerBody);

          }
}
