using Common;

namespace Subscriber
{
  public class Subscriber
  {
    public string Name { get; }
    private readonly IMessageBus _messageBus;
    private readonly Topic _topic;

    public Subscriber(string name, IMessageBus messageBus, Topic topic)
    {
      Name = name;
      _messageBus = messageBus;
      _topic = topic;
    }

    public void Subscribe()
    {
      _messageBus.Subscribe(_topic, OnMessageReceived);
    }

    public void Unsubscribe()
    {
      _messageBus.Unsubscribe(_topic, OnMessageReceived);
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
      Console.WriteLine($"{Name} received message on topic '{e.Topic}': {e.Message}");
    }
  }
}
