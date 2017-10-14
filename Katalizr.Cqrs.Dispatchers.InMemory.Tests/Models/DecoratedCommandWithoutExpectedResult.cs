using Katalizr.Cqrs.Contracts.Models;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models
{
  public class DecoratedCommandWithoutExpectedResult : ICommand
  {
    public string Text { get; set; }
  }
}
