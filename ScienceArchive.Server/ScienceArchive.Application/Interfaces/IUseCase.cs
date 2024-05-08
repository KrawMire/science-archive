namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents a generic use case in the application.
/// </summary>
/// <typeparam name="TRequest">The type of the request contract.</typeparam>
/// <typeparam name="TResponse">The type of the response contract.</typeparam>
internal interface IUseCase<in TRequest, TResponse>
{
    /// <summary>
    /// Executes the use case with the provided request contract.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request contract.</typeparam>
    /// <typeparam name="TResponse">The type of the response contract.</typeparam>
    /// <param name="contract">The request contract.</param>
    /// <returns>The response contract.</returns>
    Task<TResponse> Execute(TRequest contract);
}