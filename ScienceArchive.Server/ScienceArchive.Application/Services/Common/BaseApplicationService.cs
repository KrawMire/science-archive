using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.Services.Common;

/// <summary>
/// Base class for service implementations.
/// </summary>
internal abstract class BaseApplicationService
{
    private readonly IServiceProvider _serviceProvider;

    protected BaseApplicationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// Executes a use case with the provided request contract.
    /// </summary>
    /// <param name="contract">The request contract.</param>
    /// <typeparam name="TRequest">The type of the request contract.</typeparam>
    /// <typeparam name="TResponse">The type of the response contract.</typeparam>
    /// <returns>The response contract.</returns>
    /// <exception cref="NullReferenceException">
    /// Thrown when the use case cannot be obtained for processing the operation.
    /// </exception>
    protected async Task<TResponse> ExecuteUseCase<TRequest, TResponse>(TRequest contract)
    {
        var useCaseType = typeof(IUseCase<TRequest, TResponse>);

        if (_serviceProvider.GetService(useCaseType) is not IUseCase<TRequest, TResponse> useCase)
        {
            throw new NullReferenceException("Cannot get use case for processing the operation!");
        }

        return await useCase.Execute(contract);
    }
}