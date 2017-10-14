using System;
using Katalizr.Cqrs.Contracts.Handlers.Queries;
using Katalizr.Cqrs.Contracts.Handlers.Commands;

namespace Katalizr.Cqrs.Dispatchers.InMemory
{
  /// <summary>
  /// Factory method for creating single instances. Used to build instances of
  /// <see cref="ISynchronousQueryHandler{TQuery,TResponse}"/>,
  /// <see cref="IAsynchronousQueryHandler{TRequest,TResponse}"/>
  /// <see cref="ISynchronousCommandHandler{TQuery,TResponse}"/>,
  /// <see cref="IAsynchronousCommandHandler{TRequest,TResponse}"/>
  /// </summary>
  /// <param name="serviceType">Type of service to resolve</param>
  /// <returns>An instance of type <paramref name="serviceType" /></returns>
  public delegate object SingleInstanceFactory(Type serviceType);
}
