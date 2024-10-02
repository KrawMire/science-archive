using ScienceArchive.Application.Dtos.User;

namespace ScienceArchive.Application.Dtos.Auth.Response;

public record ConfirmUserCodeResponseDto(UserDto User, List<string> Claims);