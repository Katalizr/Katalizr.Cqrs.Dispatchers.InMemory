using Katalizr.Cqrs.Contracts.Dispatchers;
using Katalizr.Cqrs.Contracts.Handlers.Commands;
using Katalizr.Cqrs.Contracts.Handlers.Queries;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Decorators;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;
using Moq;
using StructureMap;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests
{
  public class ContainerHelper
  {
    public Container Container { get; }
    public Mock<ISynchronousCommandHandler<CommandWithoutExpectedResult>> MockedSynchronousCommandWithoutExpectedResult { get; }
    public Mock<ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>> MockedDecoratedSynchronousCommandWithoutExpectedResult { get; }
    public Mock<ISynchronousCommandHandler<CommandWithExpectedResult, string>> MockedSynchronousCommandWithExpectedResult { get; }
    public Mock<ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>> MockedDecoratedSynchronousCommandWithExpectedResult { get; }
    public Mock<ISynchronousQueryHandler<QueryWithExpectedResult, string>> MockedSynchronousQueryWithExpectedResult { get; }
    public Mock<ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>> MockedDecoratedSynchronousQueryWithExpectedResult { get; }
    public Mock<IAsynchronousCommandHandler<CommandWithoutExpectedResult>> MockedAsynchronousCommandWithoutExpectedResult { get; }
    public Mock<IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>> MockedDecoratedAsynchronousCommandWithoutExpectedResult { get; }
    public Mock<IAsynchronousCommandHandler<CommandWithExpectedResult, string>> MockedAsynchronousCommandWithExpectedResult { get; }
    public Mock<IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>> MockedDecoratedAsynchronousCommandWithExpectedResult { get; }
    public Mock<IAsynchronousQueryHandler<QueryWithExpectedResult, string>> MockedAsynchronousQueryWithExpectedResult { get; }
    public Mock<IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>> MockedDecoratedAsynchronousQueryWithExpectedResult { get; }

    public ContainerHelper()
    {
      MockedSynchronousCommandWithoutExpectedResult = new Mock<ISynchronousCommandHandler<CommandWithoutExpectedResult>>();
      MockedDecoratedSynchronousCommandWithoutExpectedResult = new Mock<ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>();
      MockedSynchronousCommandWithExpectedResult = new Mock<ISynchronousCommandHandler<CommandWithExpectedResult, string>>();
      MockedDecoratedSynchronousCommandWithExpectedResult = new Mock<ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>();
      MockedSynchronousQueryWithExpectedResult = new Mock<ISynchronousQueryHandler<QueryWithExpectedResult, string>>();
      MockedDecoratedSynchronousQueryWithExpectedResult = new Mock<ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>();
      MockedAsynchronousCommandWithoutExpectedResult = new Mock<IAsynchronousCommandHandler<CommandWithoutExpectedResult>>();
      MockedDecoratedAsynchronousCommandWithoutExpectedResult = new Mock<IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>();
      MockedAsynchronousCommandWithExpectedResult = new Mock<IAsynchronousCommandHandler<CommandWithExpectedResult, string>>();
      MockedDecoratedAsynchronousCommandWithExpectedResult = new Mock<IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>();
      MockedAsynchronousQueryWithExpectedResult = new Mock<IAsynchronousQueryHandler<QueryWithExpectedResult, string>>();
      MockedDecoratedAsynchronousQueryWithExpectedResult = new Mock<IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>();
      Container = new Container(configuration =>
      {
        configuration.For<ISynchronousCommandHandler<CommandWithoutExpectedResult>>().Use(MockedSynchronousCommandWithoutExpectedResult.Object);
        configuration.For<ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>().Use(MockedDecoratedSynchronousCommandWithoutExpectedResult.Object);
        configuration.For<ISynchronousCommandHandler<CommandWithExpectedResult, string>>().Use(MockedSynchronousCommandWithExpectedResult.Object);
        configuration.For<ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>().Use(MockedDecoratedSynchronousCommandWithExpectedResult.Object);
        configuration.For<ISynchronousQueryHandler<QueryWithExpectedResult, string>>().Use(MockedSynchronousQueryWithExpectedResult.Object);
        configuration.For<ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>().Use(MockedDecoratedSynchronousQueryWithExpectedResult.Object);
        configuration.For<IAsynchronousCommandHandler<CommandWithoutExpectedResult>>().Use(MockedAsynchronousCommandWithoutExpectedResult.Object);
        configuration.For<IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>().Use(MockedDecoratedAsynchronousCommandWithoutExpectedResult.Object);
        configuration.For<IAsynchronousCommandHandler<CommandWithExpectedResult, string>>().Use(MockedAsynchronousCommandWithExpectedResult.Object);
        configuration.For<IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>().Use(MockedDecoratedAsynchronousCommandWithExpectedResult.Object);
        configuration.For<IAsynchronousQueryHandler<QueryWithExpectedResult, string>>().Use(MockedAsynchronousQueryWithExpectedResult.Object);
        configuration.For<IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>().Use(MockedDecoratedAsynchronousQueryWithExpectedResult.Object);
        configuration.For<ISynchronousDispatcher>().Use<InMemoryDispatcher>();
        configuration.For<IAsynchronousDispatcher>().Use<InMemoryDispatcher>();
        configuration.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(context => context.GetInstance);

        configuration.For<ISynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>().DecorateAllWith<DecoratedSynchronousCommandWithoutExpectedResultHandler>();
        configuration.For<ISynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>().DecorateAllWith<DecoratedSynchronousCommandWithExpectedResultHandler>();
        configuration.For<ISynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>().DecorateAllWith<DecoratedSynchronousQueryWithExpectedResultHandler>();
        configuration.For<IAsynchronousCommandHandler<DecoratedCommandWithoutExpectedResult>>().DecorateAllWith<DecoratedAsynchronousCommandWithoutExpectedResultHandler>();
        configuration.For<IAsynchronousCommandHandler<DecoratedCommandWithExpectedResult, string>>().DecorateAllWith<DecoratedAsynchronousCommandWithExpectedResultHandler>();
        configuration.For<IAsynchronousQueryHandler<DecoratedQueryWithExpectedResult, string>>().DecorateAllWith<DecoratedAsynchronousQueryWithExpectedResultHandler>();
      });
    }
  }
}
