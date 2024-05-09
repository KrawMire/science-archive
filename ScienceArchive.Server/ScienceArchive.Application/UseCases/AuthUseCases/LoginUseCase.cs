using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.UseCases.AuthUseCases;

internal class LoginUseCase : IUseCase<LoginRequestDto, LoginResponseDto>
{
    private readonly IDbContext _dbContext;

    public LoginUseCase(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<LoginResponseDto> Execute(LoginRequestDto contract)
    {
        throw new NotImplementedException();
        // var preparedDto = new LoginRequestDto(dto.Login.Trim(), dto.Password.Trim());
        //
        // var contract = new GetUserByCredentialsContract(preparedDto.Login, preparedDto.Password);
        // var user = await _userService.GetUserByCredentials(contract);
        //
        // if (user is null)
        // {
        //     throw new Exception("User with specified credentials was not found!");
        // }
        //
        // return new(_userMapper.MapToDto(user));
    }
}