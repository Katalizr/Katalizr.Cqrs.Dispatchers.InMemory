using Katalizr.Cqrs.Contracts.Handlers.Queries;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedSynchronousQueryWithExpectedResultHandler : ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>
  {
    private ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string> Handler { get; }
    public DecoratedSynchronousQueryWithExpectedResultHandler(ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string> handler)
    {
      Handler = handler;
    }

    public string Handle(DecoratedQueryWithExpectedResult request)
    {
      return $"decorated:{Handler.Handle(request)}";
    }
  }
}
