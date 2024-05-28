using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Setup DI and build service provider
      var serviceProvider = ServiceProviderSetup.ConfigureServices();

      // Resolve Publisher
      var publisher = serviceProvider.GetService<Publisher.Publisher>();

      // Resolve Subscribers
      var subscribers = serviceProvider.GetServices<Subscriber.Subscriber>();

      // Get specific subscribers for demonstration
      var subscriber1 = subscribers.First(s => s.Name == "Subscriber1");
      var subscriber2 = subscribers.First(s => s.Name == "Subscriber2");

      subscriber1.Subscribe();
      subscriber2.Subscribe();

      // Publish initial messages
      publisher.Publish(Topic.UPPER, "Hello UPPER");

      // Unsubscribe Subscriber1 from UPPER
      subscriber1.Unsubscribe();

      // Publish messages again to see the effect of unsubscription
      publisher.Publish(Topic.UPPER, "Another message for UPPER");

      Console.ReadLine();
    }
  }
}
