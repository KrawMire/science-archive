using MediatR;
using ScienceArchive.Application.Dtos.Category.Response;

namespace ScienceArchive.Application.Dtos.Category.Request;

public record GetAllCategoriesRequestDto : IRequest<GetAllCategoriesResponseDto>;