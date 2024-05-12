using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Web.Api.Responses;

namespace ScienceArchive.Web.Api.Controllers;

[Route("api/system")]
public class SystemController : Controller
{
    private readonly ISystemApplicationService _systemApplicationService;

    public SystemController(ISystemApplicationService systemApplicationService)
    {
        _systemApplicationService = systemApplicationService ?? throw new ArgumentNullException(nameof(systemApplicationService));
    }

    [HttpGet("check-status")]
    public async Task<IActionResult> CheckStatus()
    {
        var emptyRequest = new CheckSystemStatusRequestDto();
        
        var result = await _systemApplicationService.CheckSystemStatus(emptyRequest);
        var response = new SuccessResponse(result);

        return Json(response);
    }
}