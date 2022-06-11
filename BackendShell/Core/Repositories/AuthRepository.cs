using API.Core.IRepositories;
using API.Models;
using API.Models.Auth;
using API.Projections;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public class AuthRepository: GenericRepository, IAuthRepository
    {

        private readonly UserManager<IdentityUser> _userMagager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthRepository(
             ApplicationDbContext context, 
             ILogger logger, 
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager
             ) 
            : base(context, logger)
        {
            _userMagager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserProjection> Login(LoginModel loginBody)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginBody.UserName, loginBody.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userMagager.FindByNameAsync(loginBody.UserName);
                    var userFormatted = new UserProjection(user);
                    return userFormatted;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Login method error", typeof(AuthRepository));
                return null;
            }
        }
            
            
        public async Task<IdentityResult> Register(RegisterModel registerBody)
        {
            try
            {
                var user = new IdentityUser { UserName = registerBody.UserName, Email = registerBody.Email };
                var result = await _userMagager.CreateAsync(user, registerBody.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                 return result;
                         }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Register method error", typeof(AuthRepository));
                return null;
            }
        }
    }
}
