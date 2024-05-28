using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
  [TestClass]
  public class PublisherTests
  {
    private Mock<IMessageBus> _messageBusMock;
    private Mock<IDataTransformer<string, string>> _dataTransformerMock;
    private Publisher.Publisher _publisher;

    [TestInitialize]
    public void Setup()
    {
      _messageBusMock = new Mock<IMessageBus>();
      _dataTransformerMock = new Mock<IDataTransformer<string, string>>();
      _publisher = new Publisher.Publisher(_messageBusMock.Object, _dataTransformerMock.Object);
    }

    [TestMethod]
    public void Publish_ShouldTransformMessageAndPublish()
    {
      // Arrange
      var topic = Topic.UPPER;
      var message = "test message";
      var transformedMessage = "TEST MESSAGE";
      _dataTransformerMock.Setup(transformer => transformer.Transform(message)).Returns(transformedMessage);

      // Act
      _publisher.Publish(topic, message);

      // Assert
      _dataTransformerMock.Verify(transformer => transformer.Transform(message), Times.Once);
      _messageBusMock.Verify(bus => bus.Publish(topic, transformedMessage), Times.Once);
    }
  }
}
