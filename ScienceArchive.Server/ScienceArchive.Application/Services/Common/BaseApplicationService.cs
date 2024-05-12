using MediatR;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.Services.Common;

/// <summary>
/// Base class for service implementations.
/// </summary>
internal abstract class BaseApplicationService
{
    private readonly IMediator _mediator;
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly IEventBus _eventBus;

    protected BaseApplicationService(
        IMediator mediator,
        IDbUnitOfWork dbUnitOfWork, 
        IEventBus eventBus)
    {
        _mediator = mediator;
        _dbUnitOfWork = dbUnitOfWork;
        _eventBus = eventBus;
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
        where TRequest : IRequest<TResponse>
    {
        try
        {
            await _dbUnitOfWork.StartTransactionAsync();
            
            var result = await _mediator.Send(contract);
            
            await _eventBus.HandleEvents();
            await _dbUnitOfWork.SaveAsync();
            
            return result;
        }
        catch (Exception)
        {
            await _eventBus.ClearEvents();
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
        where TRequest : IRequest<TResponse>
    {
        var result = await _mediator.Send(contract);
        await _eventBus.HandleEvents();

        return result;
    }
}