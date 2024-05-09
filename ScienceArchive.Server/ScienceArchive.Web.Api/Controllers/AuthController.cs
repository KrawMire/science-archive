using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Web.Api.Auth;
using ScienceArchive.Web.Api.Responses;

namespace ScienceArchive.Web.Api.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthManager _authManager;
    private readonly IAuthApplicationService _authService;

    public AuthController(AuthManager authManager, IAuthApplicationService authService)
    {
        _authManager = authManager;
        _authService = authService;
    }

    [HttpPost("check-admin")]
    public async Task<Response> CheckAdmin([FromBody] CheckUserClaimsRequestDto request)
    {
        request.RequiredClaims = new List<string> { "ADMIN" };
        var result = await _authService.CheckUserClaims(request);
        
        return new SuccessResponse(new
        {
            isAdmin = result.Success
        });
    }
    
    [HttpPost("sign-in")]
    public async Task<Response> SignIn([FromBody] LoginRequestDto request)
    {
        var result = await _authService.Login(request);
        var token = _authManager.GenerateToken(result.User);

        return new SuccessResponse(new
        {
            user = result.User,
            token,
        });
    }

    [HttpPost("sign-up")]
    public async Task<Response> SignUp([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.Register(request);
        return new SuccessResponse(result);
    }
}