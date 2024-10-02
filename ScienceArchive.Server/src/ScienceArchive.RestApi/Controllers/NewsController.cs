using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Rest.Api.Auth;
using ScienceArchive.Rest.Api.Responses;

namespace ScienceArchive.Rest.Api.Controllers;

[Route("api/news")]
public class NewsController : Controller
{
    private readonly INewsApplicationService _newsApplicationService;

    public NewsController(INewsApplicationService newsApplicationService)
    {
        _newsApplicationService = newsApplicationService ?? throw new ArgumentNullException(nameof(newsApplicationService));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var dto = new GetNewsByIdRequestDto(id);
        var result = await _newsApplicationService.GetNewsById(dto);
        var response = new SuccessResponse(result);

        return Json(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var emptyRequest = new GetAllNewsRequestDto();

        var result = await _newsApplicationService.GetAllNews(emptyRequest);
        var response = new SuccessResponse(result);
        
        return Json(response);
    }

    [AuthorizeClaims(AuthClaims.EditNews)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNewsRequestDto dto)
    {
        var result = await _newsApplicationService.CreateNews(dto);
        var response = new SuccessResponse(result);

        return Json(response);
    }

    [AuthorizeClaims(AuthClaims.EditNews)]
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateNewsRequestDto dto)
    {
        var result = await _newsApplicationService.UpdateNews(dto);
        var response = new SuccessResponse(result);

        return Json(response);
    }

    [AuthorizeClaims(AuthClaims.EditNews)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var dto = new DeleteNewsRequestDto(id);

        var result = await _newsApplicationService.DeleteNews(dto);
        var response = new SuccessResponse(result);

        return Json(response);
    }
}