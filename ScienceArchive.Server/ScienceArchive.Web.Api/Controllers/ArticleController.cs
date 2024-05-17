using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Web.Api.Auth;
using ScienceArchive.Web.Api.Responses;
using ScienceArchive.Web.Api.Utils;

namespace ScienceArchive.Web.Api.Controllers;

[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleApplicationService _articleService;

    public ArticleController(IArticleApplicationService articleService)
    {
        _articleService = articleService;
    }
    
    [HttpGet("by-category/{categoryId}")]
    public async Task<Response> GetVerifiedByCategoryId(string categoryId)
    {
        if (string.IsNullOrWhiteSpace(categoryId))
        {
            throw new BadHttpRequestException("Category ID was not presented");
        }
        
        var dto = new GetVerifiedArticlesByCategoryIdRequestDto(categoryId);
        var result = await _articleService.GetVerifiedArticlesByCategoryId(dto);
        return new SuccessResponse(result);
    }
    
    [Authorize]
    [HttpGet("my-articles")]
    public async Task<Response> GetUserArticles()
    {
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 401);
        }

        var result = await _articleService.GetArticlesByAuthorId(new GetArticlesByAuthorIdRequestDto(userId));
        return new SuccessResponse(result);
    }
    
    [HttpGet("by-author/{authorId}")]
    public async Task<Response> GetVerifiedByAuthorId(string? authorId)
    {
        if (string.IsNullOrWhiteSpace(authorId))
        {
            throw new BadHttpRequestException("Author ID was not presented");
        }

        var dto = new GetArticlesByAuthorIdRequestDto(authorId);
        var result = await _articleService.GetArticlesByAuthorId(dto);
        return new SuccessResponse(result);
    }
    
    [HttpGet("{id}")]
    public async Task<Response> GetById(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new BadHttpRequestException("ID was not presented");
        }

        var userId = HttpContext.GetUserIdFromToken();

        var requiredClaims = new List<string> { AuthClaims.ViewDeclinedArticles, AuthClaims.ViewNotVerifiedArticles };
        var dto = new GetArticleByIdRequestDto(id, userId, requiredClaims);
        var result = await _articleService.GetArticleById(dto);
        return new SuccessResponse(result);
    }

    [AuthorizeClaims(AuthClaims.ViewNotVerifiedArticles, AuthClaims.ViewDeclinedArticles)]
    [HttpGet("all")]
    public async Task<Response> GetAll()
    {
        var emptyRequest = new GetAllArticlesRequestDto();
        var result = await _articleService.GetAllArticles(emptyRequest);

        return new SuccessResponse(result);
    }
    
    [HttpGet]
    public async Task<Response> GetAllVerified()
    {
        var emptyRequest = new GetAllVerifiedArticlesRequestDto();

        var result = await _articleService.GetAllVerifiedArticles(emptyRequest);
        return new SuccessResponse(result);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<Response> Create([FromBody] CreateArticleRequestDto? dto)
    {
        if (dto is null)
        {
            throw new BadHttpRequestException("No data presented");
        }
        
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 403);
        }

        var populatedDto = dto with { UserId = userId };
        var result = await _articleService.CreateArticle(populatedDto);
        return new SuccessResponse(result);
    }
    
    [Authorize]
    [HttpPost("update")]
    public async Task<Response> Update([FromBody] UpdateArticleRequestDto? dto)
    {
        if (dto is null)
        {
            throw new BadHttpRequestException("No data presented");
        }
        
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 403);
        }

        var populatedDto = dto with { UserId = userId};
        var result = await _articleService.UpdateArticle(populatedDto);
        return new SuccessResponse(result);
    }

    [AuthorizeClaims(AuthClaims.ApproveArticles)]
    [HttpPost("approve")]
    public async Task<Response> Approve([FromBody] ApproveArticleRequestDto? dto)
    {
        if (dto is null)
        {
            throw new BadHttpRequestException("No data presented");
        }

        var result = await _articleService.ApproveArticle(dto);
        return new SuccessResponse(result);
    }
    
    [AuthorizeClaims(AuthClaims.DeclineArticles)]
    [HttpPost("decline")]
    public async Task<Response> Decline([FromBody] DeclineArticleRequestDto? dto)
    {
        if (dto is null)
        {
            throw new BadHttpRequestException("No data presented");
        }

        var result = await _articleService.DeclineArticle(dto);
        return new SuccessResponse(result);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<Response> Delete(string? id)
    {
        if (id is null)
        {
            throw new BadHttpRequestException("No data presented");
        }
        
        var userId = HttpContext.GetUserIdFromToken();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new BadHttpRequestException("Cannot get user ID", 403);
        }
        
        var dto = new DeleteArticleRequestDto(id, userId);
        var result = await _articleService.DeleteArticle(dto);
        return new SuccessResponse(result);
    }
}