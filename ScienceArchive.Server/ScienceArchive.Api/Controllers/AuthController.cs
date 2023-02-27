using Microsoft.AspNetCore.Mvc;

namespace ScienceArchive.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(new
            {
                success = true,
            });
        }
    }
}

