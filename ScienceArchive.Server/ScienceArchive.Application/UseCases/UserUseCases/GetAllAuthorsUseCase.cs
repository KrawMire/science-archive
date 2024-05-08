using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;

namespace ScienceArchive.Application.UseCases.UserUseCases;

internal class GetAllAuthorsUseCase : IUseCase<GetAllAuthorsRequestDto, GetAllAuthorsResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationMapper<User, AuthorDto> _userMapper;
    
    public GetAllAuthorsUseCase(IUserRepository userRepository, IApplicationMapper<User, AuthorDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<GetAllAuthorsResponseDto> Execute(GetAllAuthorsRequestDto contract)
    {
        var authors = await _userRepository.GetAll();
        var authorsDtos = authors.Select(_userMapper.MapToDto).ToList();
        
        return new GetAllAuthorsResponseDto(authorsDtos);
    }
}