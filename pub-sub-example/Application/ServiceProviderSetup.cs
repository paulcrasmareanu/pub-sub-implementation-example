using Common;
using DataTransformer;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
  public static class ServiceProviderSetup
  {
    public static IServiceProvider ConfigureServices()
    {
      var serviceCollection = new ServiceCollection();

      // Register IMessageBus implementation
      serviceCollection.AddSingleton<IMessageBus, MessageBus.MessageBus>();

      // Register Transformers
      serviceCollection.AddSingleton<UpperCaseTransformer>();

      // Register Publisher with topic-specific transformers
      serviceCollection.AddSingleton(provider =>
          new Publisher.Publisher(
              provider.GetService<IMessageBus>(),
              provider.GetService<UpperCaseTransformer>()
          ));

      // Register Subscribers
      serviceCollection.AddSingleton(provider => new Subscriber.Subscriber("Subscriber1", provider.GetService<IMessageBus>(), Topic.UPPER));
      serviceCollection.AddSingleton(provider => new Subscriber.Subscriber("Subscriber2", provider.GetService<IMessageBus>(), Topic.UPPER));

      return serviceCollection.BuildServiceProvider();
    }
  }
}
