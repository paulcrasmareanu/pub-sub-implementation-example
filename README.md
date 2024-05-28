# MessageBus Application

## Overview

This repository contains a messaging system implemented in C# that follows the publish-subscribe pattern. The application is designed to be modular and testable, with clearly defined responsibilities split across multiple libraries. The entry point of the application is a console application that sets up dependency injection and demonstrates the functionality.

The solution demonstrates the following:
- Taking input data.
- Transforming that data.
- Transporting that data to a set of subscribers.
- Displaying the transformed data by the subscribers.

## Project Architecture

### Libraries

1. **MessageBus**: Implements the core messaging functionality allowing topics to be subscribed to and messages to be published.
2. **Common**: Contains shared components and interfaces used across the application, including the `IMessageBus` interface and `Topic` enum.
3. **Subscriber**: Implements the functionality for subscribing to topics and handling received messages.
4. **Publisher**: Handles the publication of messages to topics.
5. **DataTransformer**: Contains classes responsible for transforming data before it is published.

### MessageBus Library

- **MessageBus Class**: Implements the `IMessageBus` interface, manages topic subscriptions, and invokes handlers when messages are published.

### Common Library

- **IMessageBus Interface**: Defines the contract for publishing, subscribing, and unsubscribing from topics.
- **IDataTransformer Interface**: Defines a generic interface for data transformation.
- **MessageEventArgs Class**: Defines the structure for message event arguments including topic and message content.
- **Topic Enum**: Enumerates the topics used in the messaging system.

### Subscriber Library

- **Subscriber Class**: Implements subscribing to a topic, handling received messages, and unsubscribing from a topic.

### Publisher Library

- **Publisher Class**: Uses the `IMessageBus` and `IDataTransformer` to publish transformed messages to a topic.

### DataTransformer Library

- **UpperCaseTransformer Class**: Implements the `IDataTransformer` interface to transform strings to uppercase.

## Approach Used

### Publish-Subscribe Pattern

The core design pattern used is the publish-subscribe pattern, where subscribers express interest in specific topics and publishers send messages to these topics. This decouples the components, allowing for greater flexibility and scalability.

### Modular Architecture

The project is split into multiple libraries to ensure separation of concerns and to promote code reusability. Each library has a specific responsibility and can be developed, tested, and maintained independently.

### Dependency Injection

Dependencies are injected via constructors, making the components easy to test and mock. The console application sets up the dependency injection using Microsoft.Extensions.DependencyInjection.

## Advantages

- **Separation of Concerns**: Each component has a well-defined responsibility, making the codebase more maintainable and scalable.
- **Modularity**: Splitting the project into libraries allows for independent development and testing of each component.
- **Testability**: The use of interfaces and dependency injection makes the codebase highly testable. Unit tests can mock dependencies and verify the behavior of each component in isolation.
- **Reusability**: Components such as data transformers and the message bus can be reused across different parts of the application or even in other projects.
- **Avoidance of Circular Dependencies**: By centralizing shared components in the `Common` library, the design avoids potential circular dependencies between other libraries.

## Disadvantages

- **Complexity**:  The modular structure can add complexity, which may be unnecessary for smaller projects where a simpler architecture would be sufficient.
- **Overhead**: Managing multiple projects and dependencies can be challenging, particularly for small teams or solo developers.

## Example Usage

### Console Application

The console application serves as the entry point, setting up dependency injection and demonstrating the publish-subscribe functionality.

```csharp
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Common;
using Publisher;
using Subscriber;

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
        publisher.Publish(Topic.UPPER, "hello world");

        // Unsubscribe Subscriber1 from UPPER
        subscriber1.Unsubscribe();

        // Publish messages again to see the effect of unsubscription
        publisher.Publish(Topic.UPPER, "another message for hello world");

        Console.ReadLine();
    }
}
