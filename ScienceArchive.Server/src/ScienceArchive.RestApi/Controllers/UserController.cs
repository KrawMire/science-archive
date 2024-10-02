using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Rest.Api.Responses;
using ScienceArchive.Rest.Api.Utils;

namespace ScienceArchive.Rest.Api.Controllers;

[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserApplicationService _userApplicationService;

    public UserController(IUserApplicationService userApplicationService)
    {
        _userApplicationService = userApplicationService;
    }

    [HttpGet("{id}")]
    public async Task<Response> GetById(string id)
    {
        var dto = new GetUserByIdRequestDto(id);
        var result = await _userApplicationService.GetUserById(dto);
        return new SuccessResponse(result);
    }
    
    [HttpGet]
    public async Task<Response> GetAll()
    {
        var emptyRequest = new GetAllUsersRequestDto();
        
        var result = await _userApplicationService.GetAllUsers(emptyRequest);
        return new SuccessResponse(result);
    }

    [Authorize]
    [HttpPost("update")]
    public async Task<Response> Update([FromBody] UpdateUserRequestDto dto)
    {
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 403);
        }

        var populatedDto = dto with { InitiatorUserId = userId };
        var result = await _userApplicationService.UpdateUser(populatedDto);
        
        return new SuccessResponse(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<Response> Delete(string id)
    {
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 403);
        }
        
        var dto = new DeleteUserRequestDto(id, userId);
        var result = await _userApplicationService.DeleteUser(dto);
        
        return new SuccessResponse(result);;
    }
}