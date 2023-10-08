using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Repositories;

namespace ScienceArchive.Web.Api.Auth;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class AuthorizeClaimsAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	private readonly string[] _requiredClaims;

	public AuthorizeClaimsAttribute(params string[] requiredClaims)
	{
		_requiredClaims = requiredClaims;
	}

	public void OnAuthorization(AuthorizationFilterContext context)
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
		var roleRepository = context.HttpContext.RequestServices.GetService<IRoleRepository>();

		if (roleRepository is null)
		{
			throw new NullReferenceException("Cannot get role repository service");
		}

		if (userId is null)
		{
			throw new NullReferenceException("Cannot get user ID from token");
		}
		
		var userClaims = roleRepository.GetUserClaims(UserId.CreateFromString(userId)).Result;
		
		foreach (var requiredClaim in _requiredClaims)
		{
			if (!userClaims.Exists(uc => uc.Value == requiredClaim))
			{
				context.Result = new ForbidResult();
				return;
			}
		}
	}
}