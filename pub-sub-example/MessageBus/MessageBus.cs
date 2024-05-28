using Common;

namespace MessageBus
{
  public class MessageBus : IMessageBus
  {
    private readonly Dictionary<Topic, List<EventHandler<MessageEventArgs>>> _subscriptions = new Dictionary<Topic, List<EventHandler<MessageEventArgs>>>();

    public void Publish(Topic topic, string message)
    {
      if (_subscriptions.TryGetValue(topic, out var subscriptions))
      {
        var eventArgs = new MessageEventArgs(topic, message);

        foreach (var subscription in subscriptions)
        {
          subscription?.Invoke(this, eventArgs);
        }
      }
    }

    public void Subscribe(Topic topic, EventHandler<MessageEventArgs> handler)
    {
      if (!_subscriptions.ContainsKey(topic))
      {
        _subscriptions[topic] = new List<EventHandler<MessageEventArgs>>();
      }

      // Prevent duplicate same handler for the same topic
      var handlers = _subscriptions[topic];
      if (!handlers.Contains(handler))
      {
        handlers.Add(handler);
      }
    }

    public void Unsubscribe(Topic topic, EventHandler<MessageEventArgs> handler)
    {
      if (_subscriptions.TryGetValue(topic, out var subscriptions))
      {
        subscriptions.RemoveAll(s => s == handler);
      }
    }
  }
}
