using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Web.Api.Responses;

namespace ScienceArchive.Web.Api.Controllers;

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

    [HttpPost("update")]
    [Authorize]
    public async Task<Response> Update([FromBody] UpdateUserRequestDto dto)
    {
        var result = await _userApplicationService.UpdateUser(dto);
        return new SuccessResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<Response> Delete(string id)
    {
        var dto = new DeleteUserRequestDto(id);

        var result = await _userApplicationService.DeleteUser(dto);
        return new SuccessResponse(result);;
    }
}