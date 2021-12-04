using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controller")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            return "Magic word!";
        }
    }
}