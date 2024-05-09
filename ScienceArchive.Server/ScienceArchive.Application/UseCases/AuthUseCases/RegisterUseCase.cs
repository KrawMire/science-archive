using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class RegisterUseCase : IUseCase<RegisterRequestDto, RegisterResponseDto>
{
    private readonly IDbContext _dbContext;

    public RegisterUseCase(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RegisterResponseDto> Execute(RegisterRequestDto contract)
    {
        throw new NotImplementedException();
        // var preparedDto = new SignUpRequestDto(
        //     new UserDto
        //     {
        //         Name = dto.User.Name.Trim(),
        //         Email = dto.User.Email.Trim(),
        //         Login = dto.User.Login.Trim(),
        //         RolesIds = dto.User.RolesIds
        //     },
        //     dto.Password.Trim()
        // );
        //
        // var userToCreate = _userMapper.MapToEntity(preparedDto.User);
        // userToCreate.Password.Value = preparedDto.Password;
        //
        // var contract = new CreateUserContract(userToCreate);
        // var createdUser = await _userService.Create(contract);
        //
        // return new(_userMapper.MapToDto(createdUser));
    }
}