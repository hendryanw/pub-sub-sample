using Moq;
using Newtonsoft.Json;
using PubSubSample.Domain;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PubSubSample.Infrastructure.Redis.UnitTests
{
    public class EventPublisherTests : IClassFixture<RedisMockFixture>, IDisposable
    {
        private EventPublisher _publisher;
        private RedisMockFixture _fixture;

        public EventPublisherTests(RedisMockFixture fixture)
        {
            _fixture = fixture;
            _publisher = new EventPublisher(_fixture.MockConnectionMultiplexer.Object);
        }

        [Theory]
        [InlineData("hello", "world")]
        [InlineData("location", "southeastasia")]
        [InlineData("this-first-second-third", "Hey it's me John!")]
        public void Publish_SendsPublishCommandWithGivenEvent(string key, string value)
        {
            var now = DateTime.Now;
            var configurationChangedEvent = new ConfigurationChangedEvent(key, value, now);
            _publisher.Publish(configurationChangedEvent);

            _fixture.MockConnectionMultiplexer.Verify(x => x.GetSubscriber(It.IsAny<object>()), Times.Once);
            _fixture.MockSubscriber.Verify(x => x.Publish(ConfigurationChangedEvent.EventName, JsonConvert.SerializeObject(configurationChangedEvent), It.IsAny<CommandFlags>()), Times.Once);
        }

        [Theory]
        [InlineData("hello", "world")]
        [InlineData("location", "southeastasia")]
        [InlineData("this-first-second-third", "Hey it's me John!")]
        public async Task PublishAsync_SendsPublishCommandWithGivenEvent(string key, string value)
        {
            var now = DateTime.Now;
            var configurationChangedEvent = new ConfigurationChangedEvent(key, value, now);
            await _publisher.PublishAsync(configurationChangedEvent);

            _fixture.MockConnectionMultiplexer.Verify(x => x.GetSubscriber(It.IsAny<object>()), Times.Once);
            _fixture.MockSubscriber.Verify(x => x.PublishAsync(ConfigurationChangedEvent.EventName, JsonConvert.SerializeObject(configurationChangedEvent), It.IsAny<CommandFlags>()), Times.Once);
        }

        public void Dispose()
        {
            _publisher = null;
            _fixture.Reset();
        }
    }
}
