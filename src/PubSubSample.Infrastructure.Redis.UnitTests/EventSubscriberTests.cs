using Moq;
using PubSubSample.Domain;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PubSubSample.Infrastructure.Redis.UnitTests
{
    public class EventSubscriberTests : IClassFixture<RedisMockFixture>, IDisposable
    {
        private EventSubscriber _subscriber;
        private RedisMockFixture _fixture;

        public EventSubscriberTests(RedisMockFixture fixture)
        {
            _fixture = fixture;
            _subscriber = new EventSubscriber(_fixture.MockConnectionMultiplexer.Object);
        }

        public void Dispose()
        {
            _fixture.Reset();
        }

        [Theory]
        [InlineData("hello")]
        [InlineData("world")]
        [InlineData("My name is John Doe")]
        public void Subscribe_SendsSubscribeCommandWithGivenTopic(string topic)
        {
            _subscriber.Subscribe<Event>(topic, x => { });
            _fixture.MockConnectionMultiplexer.Verify(x => x.GetSubscriber(It.IsAny<object>()), Times.Once);
            _fixture.MockSubscriber.Verify(x => x.Subscribe(topic, It.IsAny<Action<RedisChannel, RedisValue>>(), It.IsAny<CommandFlags>()), Times.Once);
        }

        [Theory]
        [InlineData("hello")]
        [InlineData("world")]
        [InlineData("My name is John Doe")]
        public async Task SubscribeAsync_SendsSubscribeCommandWithGivenTopic(string topic)
        {
            await _subscriber.SubscribeAsync<Event>(topic, x => { });
            _fixture.MockConnectionMultiplexer.Verify(x => x.GetSubscriber(It.IsAny<object>()), Times.Once);
            _fixture.MockSubscriber.Verify(x => x.SubscribeAsync(topic, It.IsAny<Action<RedisChannel, RedisValue>>(), It.IsAny<CommandFlags>()), Times.Once);
        }
    }
}
