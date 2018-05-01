using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSubSample.Domain;
using PubSubSample.Infrastructure.Redis;
using StackExchange.Redis;
using System;
using System.IO;

namespace PubSubSample.DeliveryService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var id = Guid.NewGuid().ToString();
                Console.WriteLine($"Starting the DeliveryService-{id}...");
                Console.WriteLine($"Reading the configuration...");
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                    .Build();
                var redisConnectionString = configuration.GetSection("ConnectionStrings")["Redis"];

                Console.WriteLine($"Connecting to Redis instance at [{redisConnectionString}]...");
                var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);

                Console.WriteLine($"Building dependency graph...");
                var services = new ServiceCollection();
                services.AddTransient<IEventSubscriber, EventSubscriber>();
                services.AddTransient<ConfigurationChangedListener>();
                services.AddSingleton<IConnectionMultiplexer>(redisConnectionMultiplexer);
                var serviceProvider = services.BuildServiceProvider();

                Console.WriteLine($"Resolving an instance of {nameof(ConfigurationChangedListener)}...");
                var configurationChangedListener = serviceProvider.GetService<ConfigurationChangedListener>();
                Console.WriteLine($"Listening... Press Enter to exit. {Environment.NewLine}");
                configurationChangedListener.Listen($"DeliveryService-{id}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception caught: {ex}");
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
