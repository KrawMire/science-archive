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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] CheckUserExistRequestDto request)
        {
            try
            {
                var result = await _userService.CheckUserExist(request);
                var response = new SuccessResponse(result);

                return Json(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message);
                return Json(response);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserRequestDto request)
        {
            try
            {
                var result = await _userService.Create(request);
                var response = new SuccessResponse(result);

                return Json(result);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message);
                return Json(response);
            }
        }
    }
}
