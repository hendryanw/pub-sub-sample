using Newtonsoft.Json;
using PubSubSample.Domain;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Infrastructure.Redis
{
    public class EventSubscriber : IEventSubscriber
    {
        private readonly IConnectionMultiplexer _redis;

        public EventSubscriber(IConnectionMultiplexer redisConnectionMultiplexer)
        {
            _redis = redisConnectionMultiplexer ?? throw new ArgumentNullException(nameof(redisConnectionMultiplexer));
        }

        public void Subscribe<T>(string topic, Action<T> handler) where T : Event
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(topic));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var sub = _redis.GetSubscriber();
            sub.Subscribe(topic, (channel, message) =>
            {
                handler(JsonConvert.DeserializeObject<T>(message));
            });
        }

        public async Task SubscribeAsync<T>(string topic, Action<T> handler) where T : Event
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(topic));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var sub = _redis.GetSubscriber();
            await sub.SubscribeAsync(topic, (channel, message) =>
            {
                handler(JsonConvert.DeserializeObject<T>(message));
            });
        }
    }
}
