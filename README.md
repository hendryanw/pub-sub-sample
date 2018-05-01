# pub-sub-sample
An example of publish-subscribe pattern using Redis.

# Introduction 
The example contains two loosely-coupled services which is `ConfigurationService` and `DeliveryService`. The `ConfigurationService` expose one HTTP PUT endpoint at `api/configuration` to update a key-value-pair configuration. When the user update a configuration value, the `ConfigurationService` will publish a `ConfigurationChangedEvent` to Redis. 

Redis will then forward the event to all instances of `DeliveryService` which is automatically subscribed to `ConfigurationChangedEvent` when it is started. Depending on what `DeliveryService` is doing, it can make use of the event, for example: restarting some workers so that it uses new configuration values.

![pub-sub-sample-diagram](docs/PubSubSample.png?raw=true)

# Local Testing
## Prerequisites
- Visual Studio 2017 with .NET Core Development feature installed.
- Docker for Windows or similar according to your operating system.

## Build and Test
- Clone the master branch.
- Open and build /src/PubSubSample.sln solution using Visual Studio 2017.
- Optionally, run unit tests using Test Explorer in Visual Studio 2017.
- Run a Redis docker container instance by opening a PowerShell window and run the following command `docker run -p 6379:6379 -i redis`. This will expose the redis endpoint at localhost port 6379. Make sure you update the configuration values if you are running the redis instance somewhere else.
- Run multiple instances of `DeliveryService`. You can do this by right-clicking the `PubSubSample.DeliveryService` project and click Debug > Start new instance as many times as necessary. If everything works well, you will find a console window output where `DeliveryService` is listening for events.
- Run an instance of `ConfigurationService`.
- Open the Swagger UI at http://localhost:[port]/swagger/
- Test updating a configuration by firing a PUT request to api/configuration. You can also use Swagger UI to do that.
- Check whether the `DeliveryService` receives an event when a configuration is updated. If it's working correctly, it should print something like `DeliveryService-a17f67be-b6d4-497a-beb2-d46075d29602: Configuration for key [hello] has been changed to [world] on [1/5/2018 4:36:51 PM]`
