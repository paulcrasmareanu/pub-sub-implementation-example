using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{

  [TestClass]
  public class MessageBusTests
  {
    private MessageBus.MessageBus _messageBus;
    private Mock<EventHandler<MessageEventArgs>> _handlerMock;

    [TestInitialize]
    public void Setup()
    {
      _messageBus = new MessageBus.MessageBus();
      _handlerMock = new Mock<EventHandler<MessageEventArgs>>();
    }

    [TestMethod]
    public void Subscribe_ShouldInvokeHandlerOnPublish()
    {
      // Arrange
      var topic = Topic.UPPER;
      var message = "Test Message";
      _messageBus.Subscribe(topic, _handlerMock.Object);

      // Act
      _messageBus.Publish(topic, message);

      // Assert
      _handlerMock.Verify(handler => handler(It.IsAny<object>(), It.Is<MessageEventArgs>(e => e.Message == message && e.Topic == topic)), Times.Once);
    }

    [TestMethod]
    public void Unsubscribe_ShouldNotInvokeHandlerOnPublish()
    {
      // Arrange
      var topic = Topic.UPPER;
      var message = "Test Message";
      _messageBus.Subscribe(topic, _handlerMock.Object);
      _messageBus.Unsubscribe(topic, _handlerMock.Object);

      // Act
      _messageBus.Publish(topic, message);

      // Assert
      _handlerMock.Verify(handler => handler(It.IsAny<object>(), It.IsAny<MessageEventArgs>()), Times.Never);
    }

    [TestMethod]
    public void Subscribe_ShouldNotAddDuplicateHandlers()
    {
      // Arrange
      var topic = Topic.UPPER;

      // Act
      _messageBus.Subscribe(topic, _handlerMock.Object);
      _messageBus.Subscribe(topic, _handlerMock.Object);

      // Publish to check if only one handler is called
      _messageBus.Publish(topic, "Test Message");

      // Assert
      _handlerMock.Verify(handler => handler(It.IsAny<object>(), It.IsAny<MessageEventArgs>()), Times.Once);
    }
  }
}
