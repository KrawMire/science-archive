namespace ScienceArchive.Web.Api.Auth;

public static class AuthClaims
{
    public const string AccessAdminPage = "ACCESS_ADMIN_PAGE";
    
    public const string ViewNotVerifiedArticles = "VIEW_NOT_VERIFIED_ARTICLES";
    public const string ViewDeclinedArticles = "VIEW_DECLINED_ARTICLES";
    public const string ApproveArticles = "APPROVE_ARTICLES";
    public const string DeclineArticles = "DECLINE_ARTICLES";

    public const string EditNews = "EDIT_NEWS";
}