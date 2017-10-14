using System;
using System.Collections.Concurrent;
using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Contracts.Handlers.Queries;
using Katalizr.Cqrs.Contracts.Handlers.Requests;
using Katalizr.Cqrs.Contracts.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory
{
  public static class HandlerResolver
  {
    private static readonly ConcurrentDictionary<Type, object> Handlers = new ConcurrentDictionary<Type, object>();
    private static readonly Type CommandWithoutResponseType = typeof(ICommand);
    private static readonly Type CommandWithResponseType = typeof(ICommand<>);
    private static readonly Type AsynchrononousCommandHandlerWithoutResponseType = typeof(IAsynchronousCommandHandler<>);
    private static readonly Type AsynchrononousCommandHandlerWithResponseType = typeof(IAsynchronousCommandHandler<,>);
    private static readonly Type AsynchrononousQueryHandlerWithResponseType = typeof(IAsynchronousQueryHandler<,>);
    private static readonly Type SynchrononousCommandHandlerWithoutResponseType = typeof(ISynchronousCommandHandler<>);
    private static readonly Type SynchrononousCommandHandlerWithResponseType = typeof(ISynchronousCommandHandler<,>);
    private static readonly Type SynchrononousQueryHandlerWithResponseType = typeof(ISynchronousQueryHandler<,>);

    public static ISynchronousRequestHandler<TRequest> GetSynchronousHandler<TRequest>(TRequest request, SingleInstanceFactory singleInstanceFactory) where TRequest : IRequest
    {
      try
      {
        var requestType = request.GetType();
        var handlerType = CommandWithoutResponseType.IsAssignableFrom(requestType)
          ? SynchrononousCommandHandlerWithoutResponseType.MakeGenericType(requestType)
          : throw new Exception();
        return (ISynchronousRequestHandler<TRequest>) Handlers.GetOrAdd(handlerType, singleInstanceFactory(handlerType));
      }
      catch (Exception exception)
      {
        return default(ISynchronousRequestHandler<TRequest>);
      }
    }

    public static ISynchronousRequestHandler<TRequest, TResponse> GetSynchronousHandler<TRequest, TResponse>(TRequest request, SingleInstanceFactory singleInstanceFactory) where TRequest : IRequest<TResponse>
    {
      try
      {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);
        var handlerType = CommandWithResponseType.MakeGenericType(responseType).IsAssignableFrom(requestType)
          ? SynchrononousCommandHandlerWithResponseType.MakeGenericType(requestType, responseType)
          : SynchrononousQueryHandlerWithResponseType.MakeGenericType(requestType, responseType);
        return (ISynchronousRequestHandler<TRequest, TResponse>) Handlers.GetOrAdd(handlerType, singleInstanceFactory(handlerType));
      }
      catch (Exception exception)
      {
        return default(ISynchronousRequestHandler<TRequest, TResponse>);
      }
    }
    public static IAsynchronousRequestHandler<TRequest> GetAsynchronousHandler<TRequest>(TRequest request, SingleInstanceFactory singleInstanceFactory) where TRequest : IRequest
    {
      try
      {
        var requestType = request.GetType();
        var handlerType = CommandWithoutResponseType.IsAssignableFrom(requestType)
          ? AsynchrononousCommandHandlerWithoutResponseType.MakeGenericType(requestType)
          : throw new Exception();
        return (IAsynchronousRequestHandler<TRequest>) Handlers.GetOrAdd(handlerType, singleInstanceFactory(handlerType));
      }
      catch (Exception exception)
      {
        return default(IAsynchronousRequestHandler<TRequest>);
      }
    }

    public static IAsynchronousRequestHandler<TRequest, TReponse> GetAsynchronousHandler<TRequest, TReponse>(TRequest request, SingleInstanceFactory singleInstanceFactory) where TRequest : IRequest<TReponse>
    {
      try
      {
        var requestType = request.GetType();
        var responseType = typeof(TReponse);
        var commandType = CommandWithResponseType.MakeGenericType(responseType);
        var handlerType = commandType.IsAssignableFrom(requestType)
          ? AsynchrononousCommandHandlerWithResponseType.MakeGenericType(requestType, responseType)
          : AsynchrononousQueryHandlerWithResponseType.MakeGenericType(requestType, responseType);
        return (IAsynchronousRequestHandler<TRequest, TReponse>) Handlers.GetOrAdd(handlerType, singleInstanceFactory(handlerType));
      }
      catch (Exception exception)
      {
        return default(IAsynchronousRequestHandler<TRequest, TReponse>);
      }
    }
  }
}
