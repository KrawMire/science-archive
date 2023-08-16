﻿using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Interfaces.Interactors;
using ScienceArchive.Web.Api.Responses;

namespace ScienceArchive.Web.Api.Controllers;

[Route("api/news")]
public class NewsController : Controller
{
    private readonly INewsInteractor _newsInteractor;

    public NewsController(INewsInteractor newsInteractor)
    {
        _newsInteractor = newsInteractor ?? throw new ArgumentNullException(nameof(newsInteractor));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var emptyRequest = new GetAllNewsRequestDto();

        var result = await _newsInteractor.GetAllNews(emptyRequest);
        var response = new SuccessResponse(result);
        return Json(response);
    }
}