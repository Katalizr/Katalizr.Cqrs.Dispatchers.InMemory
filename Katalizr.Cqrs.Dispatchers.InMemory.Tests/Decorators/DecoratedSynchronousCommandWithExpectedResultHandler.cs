using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedSynchronousCommandWithExpectedResultHandler : ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>
  {
    private ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string> Handler { get; }
    public DecoratedSynchronousCommandWithExpectedResultHandler(ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string> handler)
    {
      Handler = handler;
    }
    public string Handle(DecoratedCommandWithExpectedResult request)
    {
      return $"decorated:{Handler.Handle(request)}";
    }
  }
}
