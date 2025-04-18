using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        [HttpGet]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}
