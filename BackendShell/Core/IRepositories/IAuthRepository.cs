using API.Models;
using API.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<SignInResult> Login(LoginModel loginBody);
        Task<IdentityResult> Register(RegisterModel registerBody);

          }
}
