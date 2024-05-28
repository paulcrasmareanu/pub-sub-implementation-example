using Common;

namespace Publisher
{
  public class Publisher
  {
    private readonly IMessageBus _messageBus;
    private readonly IDataTransformer<string, string> _dataTransformer;

    public Publisher(IMessageBus messageBus, IDataTransformer<string, string> dataTransformer)
    {
      _messageBus = messageBus;
      _dataTransformer = dataTransformer;
    }

    public void Publish(Topic topic, string message)
    {
      var transformedMessage = _dataTransformer.Transform(message);
      _messageBus.Publish(topic, transformedMessage);
    }
  }
}
