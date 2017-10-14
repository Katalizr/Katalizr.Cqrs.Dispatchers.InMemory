using System.Threading;
using System.Threading.Tasks;
using Katalizr.Cqrs.Contracts.Handlers.Queries;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators
{
  public class DecoratedAsynchronousQueryWithExpectedResultHandler : IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>
  {
    private IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string> Handler { get; }
    public DecoratedAsynchronousQueryWithExpectedResultHandler(IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string> handler)
    {
      Handler = handler;
    }
    public Task<string> Handle(DecoratedQueryWithExpectedResult request)
    {
      return Handle(request, CancellationToken.None);
    }
    public async Task<string> Handle(DecoratedQueryWithExpectedResult request, CancellationToken cancellationToken)
    {
      var result = await Handler.Handle(request, cancellationToken);
      return $"decorated:{result}";
    }
  }
}
