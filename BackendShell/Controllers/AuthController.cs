using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // Register
        [HttpPost]
        public async Task<JsonResult> Create(Order order)
        {
            
            return new JsonResult(Problem("Something went wrong"));
        }
    }
}
