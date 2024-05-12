namespace ScienceArchive.Application.Dtos.Auth.Request;

public record ConfirmUserCodeRequestDto(string UserId, string ConfirmCode);