using ScienceArchive.Application.Dtos.User;

namespace ScienceArchive.Application.Dtos.Auth.Response;

public record GetUserDataResponseDto(UserDto User, List<string> Claims);