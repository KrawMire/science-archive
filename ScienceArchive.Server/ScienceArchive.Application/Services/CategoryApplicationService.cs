using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Dtos.Category.Response;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class CategoryApplicationService : BaseApplicationService, ICategoryApplicationService
{ 
	public CategoryApplicationService(IServiceProvider serviceProvider) : base(serviceProvider) { }
	
	/// <inheritdoc/>
	public Task<GetAllCategoriesResponseDto> GetAllCategories(GetAllCategoriesRequestDto dto)
	{
		return ExecuteUseCase<GetAllCategoriesRequestDto, GetAllCategoriesResponseDto>(dto);
	}
}