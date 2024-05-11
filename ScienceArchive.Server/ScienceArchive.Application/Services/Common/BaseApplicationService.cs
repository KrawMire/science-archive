using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.Services.Common;

/// <summary>
/// Base class for service implementations.
/// </summary>
internal abstract class BaseApplicationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IEventBus _eventBus;

    protected BaseApplicationService(
        IServiceProvider serviceProvider, 
        IDbUnitOfWork dbUnitOfWork, 
        IEventBus eventBus)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _dbUnitOfWork = dbUnitOfWork ?? throw new ArgumentNullException(nameof(dbUnitOfWork));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    /// <summary>
    /// Executes a use case with the provided request contract in a transactional manner.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request contract.</typeparam>
    /// <typeparam name="TResponse">The type of the response contract.</typeparam>
    /// <param name="contract">The request contract.</param>
    /// <returns>The response contract.</returns>
    /// <exception cref="NullReferenceException">Thrown when the use case cannot be obtained for processing the operation.</exception>
    protected async Task<TResponse> ExecuteTransactionalUseCase<TRequest, TResponse>(TRequest contract)
    {
        var useCaseType = typeof(IUseCase<TRequest, TResponse>);

        if (_serviceProvider.GetService(useCaseType) is not IUseCase<TRequest, TResponse> useCase)
        {
            throw new NullReferenceException("Cannot get use case for processing the operation!");
        }

        try
        {
            await _dbUnitOfWork.StartTransactionAsync();
            
            var result = await useCase.Execute(contract);
            
            await _eventBus.HandleEvents();
            await _dbUnitOfWork.SaveAsync();
            
            return result;
        }
        catch (Exception)
        {
            await _dbUnitOfWork.RollbackAsync();
            throw;
        }
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

        var result = await useCase.Execute(contract);
        await _eventBus.HandleEvents();

        return result;
    }
}