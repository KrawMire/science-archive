using MediatR;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents a generic use case in the application.
/// </summary>
/// <typeparam name="TRequest">The type of the request contract.</typeparam>
/// <typeparam name="TResponse">The type of the response contract.</typeparam>
internal interface IUseCase<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{ }