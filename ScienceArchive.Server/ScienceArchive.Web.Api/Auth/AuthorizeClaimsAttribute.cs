using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Interfaces.Services;

namespace ScienceArchive.Web.Api.Auth;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeClaimsAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	private readonly string[] _requiredClaims;

	public AuthorizeClaimsAttribute(params string[] requiredClaims)
	{
		_requiredClaims = requiredClaims;
	}

	public async void OnAuthorization(AuthorizationFilterContext context)
	{
		if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated)
		{
			context.Result = new UnauthorizedResult();
			return;
		}

		if (_requiredClaims.Length == 0)
		{
			return;
		}
		
		var userId = context.HttpContext.User.FindFirst("UserId")?.Value;
		var authInteractor = context.HttpContext.RequestServices.GetService<IAuthApplicationService>();

		if (authInteractor is null)
		{
			throw new NullReferenceException("Cannot get auth interactor");
		}

		if (userId is null)
		{
			throw new NullReferenceException("Cannot get user ID from token");
		}

		var dto = new CheckUserClaimsRequestDto
		{
			UserId = userId,
			RequiredClaims = _requiredClaims.ToList()
		};
		
		var result = await authInteractor.CheckUserClaims(dto);

		if (!result.Success)
		{
			context.Result = new ForbidResult();
		}
	}
}