using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class GetUserDataUseCase : IUseCase<GetUserDataRequestDto, GetUserDataResponseDto>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IApplicationMapper<User, UserDto> _userMapper;

    public GetUserDataUseCase(IDbUnitOfWork dbUnitOfWork, IApplicationMapper<User, UserDto> userMapper)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _userMapper = userMapper;
    }

    public async Task<GetUserDataResponseDto> Handle(GetUserDataRequestDto request, CancellationToken cancellationToken)
    {
        var userId = UserId.CreateFromString(request.UserId);
        var user = await _dbUnitOfWork.UserRepository.GetById(userId);

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }

        var claims = await _dbUnitOfWork.RoleRepository.GetUserClaims(userId);

        return new GetUserDataResponseDto(
            _userMapper.MapToDto(user),
            claims.Select(c => c.Value).ToList());
    }
}