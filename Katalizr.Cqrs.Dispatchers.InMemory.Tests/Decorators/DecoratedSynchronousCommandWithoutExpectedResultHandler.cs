using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedSynchronousCommandWithoutExpectedResultHandler : ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>
  {
    private ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult> Handler { get; }
    public DecoratedSynchronousCommandWithoutExpectedResultHandler(ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult> handler)
    {
      Handler = handler;
    }
    public void Handle(DecoratedCommandWithoutExpectedResult request)
    {
      request.Text = $"decorated:{request.Text}";
      Handler.Handle(request);
    }
  }
}
