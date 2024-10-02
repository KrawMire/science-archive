using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Rest.Api.Responses;

namespace ScienceArchive.Rest.Api.Controllers;

[Route("api/categories")]
public class CategoryController : Controller
{
	private readonly ICategoryApplicationService _categoryService;
	
	public CategoryController(ICategoryApplicationService categoryService)
	{
		_categoryService = categoryService;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var emptyDto = new GetAllCategoriesRequestDto();
		var result = await _categoryService.GetAllCategories(emptyDto);
		var response = new SuccessResponse(result);
		
		return Json(response);
	} 
}