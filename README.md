# pub-sub-sample
An example of publish-subscribe pattern using Redis.

# Introduction 
The example contains two loosely-coupled services which is `ConfigurationService` and `DeliveryService`. The `ConfigurationService` expose one HTTP PUT endpoint at `api/configuration` to update a key-value-pair configuration. When the user update a configuration value, the `ConfigurationService` will publish a `ConfigurationChangedEvent` to Redis. Redis will then forward the event to all instances of `DeliveryService` which is subscribed to `ConfigurationChangedEvent` when it is started.

![pub-sub-sample-diagram](docs/PubSubSample.png?raw=true)

# Local Testing
## Prerequisites
- Install Visual Studio 2017 with .NET Core Development feature installed.
- Docker for Windows or similar according to your operating system.

## Build and Test
- Clone the master branch.
- Open and build /src/PubSubSample.sln solution using Visual Studio 2017.
- Optionally, run unit tests using Test Explorer in Visual Studio 2017.
- Run Redis docker image. Open PowerShell and run the following command `docker run -p 6379:6379 -i redis`.
- Run multiple instances of `DeliveryService`. You can do this by right-clicking the `PubSubSample.DeliveryService` project and click Debug > Start new instance as many times as necessary. If everything works well, you will find a console window output where `DeliveryService` is listening for events.
- Run an instance of `ConfigurationService`.
- Open the Swagger UI at http://localhost:59165/swagger/
- Test updating a configuration by creating a PUT request to api/configuration. You can also use Swagger UI to do that.
- Check whether the `DeliveryService` receives an event when a configuration is updated.
