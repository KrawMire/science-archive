using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Rest.Api.Responses;

namespace ScienceArchive.Rest.Api.Controllers;

[Route("api/roles")]
public class RoleController : Controller
{
    private readonly IRoleApplicationService _roleApplicationService;

    public RoleController(IRoleApplicationService roleApplicationService)
    {
        _roleApplicationService = roleApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var emptyRequest = new GetAllRolesRequestDto();

        var result = await _roleApplicationService.GetAllRoles(emptyRequest);
        var response = new SuccessResponse(result);
        return Json(response);
    }
}