namespace Common
{
  public class MessageEventArgs : EventArgs
  {
    public Topic Topic { get; set; }
    public string Message { get; }

    public MessageEventArgs(Topic topic, string message)
    {
      Topic = topic;
      Message = message;
    }
  }
}
