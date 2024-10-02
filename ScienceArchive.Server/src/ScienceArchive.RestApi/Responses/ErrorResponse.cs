namespace ScienceArchive.Rest.Api.Responses;

public class ErrorResponse : Response
{
    public ErrorResponse(string errorMessage)
    {
        Error = errorMessage;
    }
}