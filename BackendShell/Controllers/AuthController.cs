using API.Core.IConfiguration;
using API.Models;
using API.Models.Auth;
using API.Projections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Register
        [HttpPost("/Register")]
        public async Task<ActionResult> Register(RegisterModel registerBody)
        {
            if (!ModelState.IsValid) return BadRequest();
           var result  = await _unitOfWork.Auth.Register(registerBody);
            if(result != null)
            {
                if (result.Succeeded) {
                    var user = await _unitOfWork.UserManager.FindByNameAsync(registerBody.UserName);
                    UserProjection userFormatted = new UserProjection(user);
                    return Ok(userFormatted);
                } 
                return BadRequest(result.Errors);
            }
            return Problem("Something went wrong");

        }

        // Login
        [HttpPost("/Login")]
        public async Task<ActionResult> Login(LoginModel loginBody)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _unitOfWork.Auth.Login(loginBody);
            if (user != null)
            {
                return Ok(user);
            }
                return BadRequest();
        }
    }
}
