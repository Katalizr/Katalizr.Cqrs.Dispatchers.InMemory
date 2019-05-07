using System.Threading;
using System.Threading.Tasks;
using Katalizr.Cqrs.Contracts.Dispatchers;
using Katalizr.Cqrs.Dispatchers.InMemory.Tests.Models;
using Moq;
using NFluent;
using Xunit;

namespace Katalizr.Cqrs.Dispatchers.InMemory.Tests
{
  public class InMemoryDispatcherTests
  {
    #region Commands

    #region Synchronous

    [Fact]
    public void
      ShouldInvokeTheCommandHandlerWhenACommandWithoutExpectedResultIsDispatched()
    {
      // Arranges
      var command = new CommandWithoutExpectedResult();
      var containerHelper = new ContainerHelper();
      var dispatcher = containerHelper.Container
        .GetInstance<ISynchronousDispatcher>();

      // Acts
      dispatcher.Dispatch(command);

      // Asserts
      containerHelper.MockedSynchronousCommandWithoutExpectedResult
        .Verify(method => method.Handle(command), Times.Once);
    }

    [Fact]
    public void
      ShouldInvokeTheDecoratedCommandHandlerWhenACommandWithoutExpectedResultIsDispatched()
    {
      // Arranges
      var command =
        new DecoratedCommandWithoutExpectedResult() {Text = "string"};
      var containerHelper = new ContainerHelper();
      var textToAsert = string.Empty;
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedSynchronousCommandWithoutExpectedResult
        .Setup(method => method.Handle(It.IsAny<DecoratedCommandWithoutExpectedResult>()))
        .Callback<DecoratedCommandWithoutExpectedResult>(item => textToAsert = item.Text);
      var dispatcher = containerHelper.Container
        .GetInstance<ISynchronousDispatcher>();

      // Acts
      dispatcher.Dispatch(command);

      // Asserts
      Check.That(textToAsert)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedSynchronousCommandWithoutExpectedResult
        .Verify(method => method.Handle(command), Times.Once);
    }

    [Fact]
    public void
      ShouldInvokeTheCommandHandlerWhenACommandWithExpectedResultIsDispatched()
    {
      // Arranges
      var command = new CommandWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "string";
      containerHelper.MockedSynchronousCommandWithExpectedResult
        .Setup(method => method.Handle(command))
        .Returns(expectedResult);
      var dispatcher = containerHelper.Container
        .GetInstance<ISynchronousDispatcher>();

      // Acts
      var result =
        dispatcher.Dispatch<CommandWithExpectedResult, string>(command);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedSynchronousCommandWithExpectedResult
        .Verify(method => method.Handle(command), Times.Once);
    }

    [Fact]
    public void
      ShouldInvokeTheDecoratedCommandHandlerWhenACommandWithExpectedResultIsDispatched()
    {
      // Arranges
      var command = new DecoratedCommandWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedSynchronousCommandWithExpectedResult
        .Setup(method => method.Handle(command))
        .Returns("string");
      var dispatcher = containerHelper.Container.GetInstance<ISynchronousDispatcher>();

      // Acts
      var result =
        dispatcher
          .Dispatch<DecoratedCommandWithExpectedResult, string>(command);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedSynchronousCommandWithExpectedResult
        .Verify(method => method.Handle(command), Times.Once);
    }

    #endregion

    #region Asynchronous

    [Fact]
    public async Task
      ShouldInvokeTheAsynchronousCommandHandlerWhenACommandWithoutExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var command = new CommandWithoutExpectedResult();
      var containerHelper = new ContainerHelper();
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      await dispatcher.Dispatch(command);

      // Asserts
      containerHelper.MockedAsynchronousCommandWithoutExpectedResult.Verify(method => method.Handle(command, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task
      ShouldInvokeTheDecoratedAsynchronousCommandHandlerWhenACommandWithoutExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var command = new DecoratedCommandWithoutExpectedResult() {Text = "string"};
      var containerHelper = new ContainerHelper();
      var textToAsert = string.Empty;
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedAsynchronousCommandWithoutExpectedResult
        .Setup(method => method.Handle(It.IsAny<DecoratedCommandWithoutExpectedResult>(), CancellationToken.None))
        .Returns(Task.CompletedTask)
        .Callback<DecoratedCommandWithoutExpectedResult, CancellationToken>((receivedCommand, cancellationToken) => textToAsert = receivedCommand.Text);
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      await dispatcher.Dispatch(command);

      // Asserts
      Check.That(textToAsert)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedAsynchronousCommandWithoutExpectedResult
        .Verify(method => method.Handle(command, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task
      ShouldInvokeTheAsynchronousCommandHandlerWhenACommandWithExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var command = new CommandWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "string";
      containerHelper.MockedAsynchronousCommandWithExpectedResult
        .Setup(method => method.Handle(command, CancellationToken.None))
        .Returns(Task.FromResult(expectedResult));
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      var result =
        await dispatcher.Dispatch<CommandWithExpectedResult, string>(command);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedAsynchronousCommandWithExpectedResult.Verify(method => method.Handle(command, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task
      ShouldInvokeTheDecoratedAsynchronousCommandHandlerWhenACommandWithExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var command = new DecoratedCommandWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedAsynchronousCommandWithExpectedResult
        .Setup(method => method.Handle(command, CancellationToken.None))
        .Returns(Task.FromResult("string"));
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      var result = await dispatcher.Dispatch<DecoratedCommandWithExpectedResult, string>(command);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedAsynchronousCommandWithExpectedResult
        .Verify(method => method.Handle(command, CancellationToken.None), Times.Once);
    }

    #endregion

    #endregion

    #region Queries

    #region Synchronous

    [Fact]
    public void
      ShouldInvokeTheQueryHandlerWhenAQueryWithExpectedResultIsDispatched()
    {
      // Arranges
      var query = new QueryWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "string";
      containerHelper.MockedSynchronousQueryWithExpectedResult
        .Setup(method => method.Handle(query))
        .Returns(expectedResult);
      var dispatcher = containerHelper.Container.GetInstance<ISynchronousDispatcher>();

      // Acts
      var result = dispatcher.Dispatch<QueryWithExpectedResult, string>(query);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedSynchronousQueryWithExpectedResult.Verify(method => method.Handle(query), Times.Once);
    }

    [Fact]
    public void
      ShouldInvokeTheDecoratedQueryHandlerWhenAQueryWithExpectedResultIsDispatched()
    {
      // Arranges
      var query = new DecoratedQueryWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedSynchronousQueryWithExpectedResult
        .Setup(method => method.Handle(query))
        .Returns("string");
      var dispatcher = containerHelper.Container.GetInstance<ISynchronousDispatcher>();

      // Acts
      var result = dispatcher.Dispatch<DecoratedQueryWithExpectedResult, string>(query);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedSynchronousQueryWithExpectedResult.Verify(method => method.Handle(query), Times.Once);
    }

    #endregion

    #region Asynchronous

    [Fact]
    public async Task
      ShouldInvokeTheAsynchronousQueryHandlerWhenAQueryWithExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var query = new QueryWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "string";
      containerHelper.MockedAsynchronousQueryWithExpectedResult
        .Setup(method => method.Handle(query, CancellationToken.None))
        .Returns(Task.FromResult(expectedResult));
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      var result = await dispatcher.Dispatch<QueryWithExpectedResult, string>(query);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedAsynchronousQueryWithExpectedResult.Verify(method => method.Handle(query, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task
      ShouldInvokeTheDecoratedAsynchronousQueryHandlerWhenAQueryWithExpectedResultIsDispatchedAsynchronously()
    {
      // Arranges
      var query = new DecoratedQueryWithExpectedResult();
      var containerHelper = new ContainerHelper();
      var expectedResult = "decorated:string";
      containerHelper.MockedDecoratedAsynchronousQueryWithExpectedResult
        .Setup(method => method.Handle(query, CancellationToken.None))
        .Returns(Task.FromResult("string"));
      var dispatcher = containerHelper.Container.GetInstance<IAsynchronousDispatcher>();

      // Acts
      var result = await dispatcher.Dispatch<DecoratedQueryWithExpectedResult, string>(query);

      // Asserts
      Check.That(result)
        .IsEqualTo(expectedResult);
      containerHelper.MockedDecoratedAsynchronousQueryWithExpectedResult
        .Verify(method => method.Handle(query, CancellationToken.None), Times.Once);
    }

    #endregion

    #endregion
  }
}
