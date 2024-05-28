namespace Common
{
  public interface IMessageBus
  {
    void Publish(Topic topic, string message);
    void Subscribe(Topic topic, EventHandler<MessageEventArgs> handler);
    void Unsubscribe(Topic topic, EventHandler<MessageEventArgs> handler);
  }
}
