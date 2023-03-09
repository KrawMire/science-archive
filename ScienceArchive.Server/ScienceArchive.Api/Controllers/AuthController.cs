using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Core.Interfaces.Services;
using ScienceArchive.Api.Responses;
using ScienceArchive.Core.Dtos.UserRequest;

namespace ScienceArchive.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto request)
        {
            try
            {
                var result = await _userService.Create(request);
                Response response = new SuccessResponse(result);

                return Json(result);
            }
            catch (Exception e)
            {
                Response response = new ErrorResponse(e.Message);

                return Json(response);
            }
        }
    }
}
