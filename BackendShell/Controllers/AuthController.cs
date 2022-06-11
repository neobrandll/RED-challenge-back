using API.Core.IConfiguration;
using API.Models;
using API.Models.Auth;
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
        public async Task<JsonResult> Register(RegisterModel registerBody)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());
           var result  = await _unitOfWork.Auth.Register(registerBody);
            if(result != null)
            {
                if (result.Succeeded) return new JsonResult(Ok());
                return new JsonResult(BadRequest(result.Errors));
            }
            return new JsonResult(Problem("Something went wrong"));

        }

        // Login
        [HttpPost("/Login")]
        public async Task<JsonResult> Login(LoginModel loginBody)
        {
            if (!ModelState.IsValid) return new JsonResult(BadRequest());
            var result = await _unitOfWork.Auth.Login(loginBody);
            if (result != null)
            {
                if (result.Succeeded) return new JsonResult(Ok());
                return new JsonResult(BadRequest());
            }
            return new JsonResult(Problem("Something went wrong"));
        }
    }
}
