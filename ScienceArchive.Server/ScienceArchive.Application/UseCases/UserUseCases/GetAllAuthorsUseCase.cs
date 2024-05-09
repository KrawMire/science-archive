using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetAllAuthorsUseCase : IUseCase<GetAllAuthorsRequestDto, GetAllAuthorsResponseDto>
{
    private readonly IDbContext _dbContext;
    private readonly IApplicationMapper<User, AuthorDto> _userMapper;
    
    public GetAllAuthorsUseCase(IApplicationMapper<User, AuthorDto> userMapper, IDbContext dbContext)
    {
        _userMapper = userMapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetAllAuthorsResponseDto> Execute(GetAllAuthorsRequestDto contract)
    {
        var authors = await _dbContext.UserRepository.GetAll();
        var authorsDtos = authors.Select(_userMapper.MapToDto).ToList();
        
        return new GetAllAuthorsResponseDto(authorsDtos);
    }
}