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
        public async Task<ActionResult> Register(RegisterModel registerBody)
        {
            if (!ModelState.IsValid) return BadRequest();
           var result  = await _unitOfWork.Auth.Register(registerBody);
            if(result != null)
            {
                if (result.Succeeded) return Ok();
                return BadRequest(result.Errors);
            }
            return Problem("Something went wrong");

        }

        // Login
        [HttpPost("/Login")]
        public async Task<ActionResult> Login(LoginModel loginBody)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _unitOfWork.Auth.Login(loginBody);
            if (result != null)
            {
                if (result.Succeeded) return Ok();
                return BadRequest();
            }
            return Problem("Something went wrong");
        }
    }
}
