using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
  [TestClass]
  public class SubscriberTests
  {
    private Mock<IMessageBus> _messageBusMock;
    private Subscriber.Subscriber _subscriber;

    [TestInitialize]
    public void Setup()
    {
      _messageBusMock = new Mock<IMessageBus>();
      _subscriber = new Subscriber.Subscriber("TestSubscriber", _messageBusMock.Object, Topic.UPPER);
    }

    [TestMethod]
    public void Subscribe_ShouldSubscribeToTopic()
    {
      // Act
      _subscriber.Subscribe();

      // Assert
      _messageBusMock.Verify(bus => bus.Subscribe(Topic.UPPER, It.IsAny<EventHandler<MessageEventArgs>>()), Times.Once);
    }

    [TestMethod]
    public void Unsubscribe_ShouldUnsubscribeFromTopic()
    {
      // Act
      _subscriber.Unsubscribe();

      // Assert
      _messageBusMock.Verify(bus => bus.Unsubscribe(Topic.UPPER, It.IsAny<EventHandler<MessageEventArgs>>()), Times.Once);
    }
  }
}
