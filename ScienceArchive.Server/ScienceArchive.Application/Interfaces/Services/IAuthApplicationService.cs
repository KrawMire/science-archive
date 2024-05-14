using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;

namespace ScienceArchive.Application.Interfaces.Services;

/// <summary>
/// Application auth service
/// </summary>
public interface IAuthApplicationService
{
    /// <summary>
    /// Login user, i.e. user authentication
    /// </summary>
    /// <param name="dto">DTO contract to authenticate user</param>
    /// <returns>Response DTO</returns>
    Task<LoginResponseDto> Login(LoginRequestDto dto);

    /// <summary>
    /// Sign up user, i.e. create new user
    /// </summary>
    /// <param name="dto">DTO contract to sign up user</param>
    /// <returns>Response DTO</returns>
    Task<RegisterResponseDto> Register(RegisterRequestDto dto);

    /// <summary>
    /// Confirm user code, i.e. verify the confirmation code for user registration
    /// </summary>
    /// <param name="dto">DTO contract containing the user ID and confirmation code</param>
    /// <returns>Response DTO containing the confirmed user details</returns>
    Task<ConfirmUserCodeResponseDto> ConfirmUserCode(ConfirmUserCodeRequestDto dto);

    /// <summary>
    /// Resend the confirmation code for user registration
    /// </summary>
    /// <param name="dto">DTO contract containing the user ID</param>
    /// <returns>Response DTO containing the user ID</returns>
    Task<ResendConfirmationCodeResponseDto> ResendConfirmCode(ResendConfirmationCodeRequestDto dto);

    /// <summary>
    /// Retrieves user data for the given user ID.
    /// </summary>
    /// <param name="dto">DTO contract containing the user ID</param>
    /// <returns>Response DTO containing the user data</returns>
    Task<GetUserDataResponseDto> GetUserData(GetUserDataRequestDto dto);

    /// <summary>
    /// Check user claims
    /// </summary>
    /// <param name="dto">DTO contract to check user claims</param>
    /// <returns>Response DTO</returns>
    Task<CheckUserClaimsResponseDto> CheckUserClaims(CheckUserClaimsRequestDto dto);
}