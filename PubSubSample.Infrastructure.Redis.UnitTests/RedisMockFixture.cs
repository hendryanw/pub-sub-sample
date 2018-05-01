using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Infrastructure.Redis.UnitTests
{
    public class RedisMockFixture
    {
        private Mock<ISubscriber> _mockSubscriber;
        private Mock<IConnectionMultiplexer> _mockConnectionMultiplexer;

        public Mock<ISubscriber> MockSubscriber => _mockSubscriber;

        public Mock<IConnectionMultiplexer> MockConnectionMultiplexer => _mockConnectionMultiplexer;

        public RedisMockFixture()
        {
            _mockSubscriber = new Mock<ISubscriber>();
            _mockConnectionMultiplexer = new Mock<IConnectionMultiplexer>();
            _mockConnectionMultiplexer.Setup(x => x.GetSubscriber(It.IsAny<object>()))
                .Returns(_mockSubscriber.Object);
        }

        public void Reset()
        {
            _mockSubscriber = new Mock<ISubscriber>();
            _mockSubscriber.Setup(x => x.PublishAsync(It.IsAny<RedisChannel>(), It.IsAny<RedisValue>(), It.IsAny<CommandFlags>()))
                .Returns(Task.FromResult((long)1));

            _mockConnectionMultiplexer = new Mock<IConnectionMultiplexer>();
            _mockConnectionMultiplexer.Setup(x => x.GetSubscriber(It.IsAny<object>()))
                .Returns(_mockSubscriber.Object);
        }
    }
}
