using MediatR;
using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Dtos.Category.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Application service for managing categories.
/// </summary>
internal class CategoryApplicationService : BaseApplicationService, ICategoryApplicationService
{ 
	public CategoryApplicationService(IMediator mediator, IDbUnitOfWork dbUnitOfWork, IDomainEventBus eventBus) 
		: base(mediator, dbUnitOfWork, eventBus) { }

	/// <inheritdoc/>
	public Task<GetAllCategoriesResponseDto> GetAllCategories(GetAllCategoriesRequestDto dto)
	{
		return ExecuteUseCase<GetAllCategoriesRequestDto, GetAllCategoriesResponseDto>(dto);
	}
}