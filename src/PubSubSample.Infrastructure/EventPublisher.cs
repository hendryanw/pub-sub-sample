using Newtonsoft.Json;
using PubSubSample.Domain;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace PubSubSample.Infrastructure.Redis
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IConnectionMultiplexer _redis;

        public EventPublisher(IConnectionMultiplexer redisConnectionMultiplexer)
        {
            _redis = redisConnectionMultiplexer ?? throw new ArgumentNullException(nameof(redisConnectionMultiplexer));
        }

        public void Publish(Event eventObject)
        {
            if (eventObject == null)
            {
                throw new ArgumentNullException(nameof(eventObject));
            }

            var sub = _redis.GetSubscriber();
            sub.Publish(eventObject.Name, JsonConvert.SerializeObject(eventObject));
        }

        public async Task PublishAsync(Event eventObject)
        {
            if (eventObject == null)
            {
                throw new ArgumentNullException(nameof(eventObject));
            }

            var sub = _redis.GetSubscriber();
            await sub.PublishAsync(eventObject.Name, JsonConvert.SerializeObject(eventObject));
        }
    }
}
