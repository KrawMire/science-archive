using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Web.Api.Auth;
using ScienceArchive.Web.Api.Responses;
using ScienceArchive.Web.Api.Utils;

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
    public async Task<Response> CheckAdmin()
    {
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 401);
        }
        
        var request = new CheckUserClaimsRequestDto(userId, new List<string> { "ADMIN" });
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
        
        Response.Cookies.Append("Authorization", token);
        
        return new SuccessResponse(result);
    }

    [HttpPost("sign-up")]
    public async Task<Response> SignUp([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.Register(request);
        return new SuccessResponse(result);
    }

    [HttpPost("confirm")]
    public async Task<Response> Confirm([FromBody] ConfirmUserCodeRequestDto request)
    {
        var result = await _authService.ConfirmUserCode(request);
        var token = _authManager.GenerateToken(result.User);
        Response.Cookies.Append("Authorization", token);
        
        return new SuccessResponse(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<Response> GetUserData()
    {
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 401);
        }

        var request = new GetUserDataRequestDto(userId);
        var result = await _authService.GetUserData(request);

        return new SuccessResponse(result);
    }
    
    [HttpPost("resend-code")]
    public async Task<Response> ResendCode([FromBody] ResendConfirmationCodeRequestDto request)
    {
        var result = await _authService.ResendConfirmCode(request);
        return new SuccessResponse(result);
    }
}