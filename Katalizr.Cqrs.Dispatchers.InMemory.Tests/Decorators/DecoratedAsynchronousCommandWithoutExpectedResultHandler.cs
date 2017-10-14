using System.Threading;
using System.Threading.Tasks;
using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedAsynchronousCommandWithoutExpectedResultHandler : IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>
  {
    private IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult> Handler { get; }
    public DecoratedAsynchronousCommandWithoutExpectedResultHandler(IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult> handler)
    {
      Handler = handler;
    }
    public Task Handle(DecoratedCommandWithoutExpectedResult request)
    {
      return Handle(request, CancellationToken.None);
    }
    public async Task Handle(DecoratedCommandWithoutExpectedResult request, CancellationToken cancellationToken)
    {
      request.Text = $"decorated:{request.Text}";
      await Handler.Handle(request, cancellationToken);
    }
  }
}
