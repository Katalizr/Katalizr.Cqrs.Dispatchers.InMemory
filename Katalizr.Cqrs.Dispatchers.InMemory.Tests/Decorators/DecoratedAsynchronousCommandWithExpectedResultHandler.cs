using System.Threading;
using System.Threading.Tasks;
using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedAsynchronousCommandWithExpectedResultHandler : IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>
  {
    private IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string> Handler { get; }
    public DecoratedAsynchronousCommandWithExpectedResultHandler(IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string> handler)
    {
      Handler = handler;
    }
    public Task<string> Handle(DecoratedCommandWithExpectedResult request)
    {
      return Handle(request, CancellationToken.None);
    }
    public async Task<string> Handle(DecoratedCommandWithExpectedResult request, CancellationToken cancellationToken)
    {
      return $"decorated:{await Handler.Handle(request, cancellationToken)}";
    }
  }
}
