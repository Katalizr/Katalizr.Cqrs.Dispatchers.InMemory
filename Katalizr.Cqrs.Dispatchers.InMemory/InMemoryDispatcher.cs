using System;
using System.Threading;
using System.Threading.Tasks;
using Katalizr.Cqrs.Contracts.Dispatchers;
using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Contracts.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory
{
  public class InMemoryDispatcher : ISynchronousDispatcher, IAsynchronousDispatcher
  {
    private SingleInstanceFactory SingleInstanceFactory { get; }
    public InMemoryDispatcher(SingleInstanceFactory singleInstanceFactory)
    {
      SingleInstanceFactory = singleInstanceFactory;
    }
    void ISynchronousDispatcher.Dispatch<TRequest>(TRequest request)
    {
      HandlerResolver.GetSynchronousHandler(request, SingleInstanceFactory)?.Handle(request);
    }
    public Task Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest
    {
      return HandlerResolver.GetAsynchronousHandler(request, SingleInstanceFactory)?.Handle(request, cancellationToken);
    }
    Task<TResponse> IAsynchronousDispatcher.Dispatch<TRequest, TResponse>(TRequest request)
    {
      return Dispatch<TRequest, TResponse>(request, CancellationToken.None);
    }
    public Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
      return HandlerResolver.GetAsynchronousHandler<TRequest, TResponse>(request, SingleInstanceFactory)?.Handle(request, cancellationToken);
    }
    Task IAsynchronousDispatcher.Dispatch<TRequest>(TRequest request)
    {
      return Dispatch(request, CancellationToken.None);
    }
    TResponse ISynchronousDispatcher.Dispatch<TRequest, TResponse>(TRequest request)
    {
      var synchronousRequestHandler = HandlerResolver.GetSynchronousHandler<TRequest, TResponse>(request, SingleInstanceFactory);
      if (synchronousRequestHandler != null)
      {
        return synchronousRequestHandler.Handle(request);
      }
      throw new NotImplementedException();
    }
  }
}
