using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Domain
{
    /// <summary>
    /// An interface to publish events.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publish an event to be consumed by subscribers.
        /// </summary>
        /// <param name="eventObject">The event.</param>
        void Publish(Event eventObject);

        /// <summary>
        /// Publish an event to be consumed by subscribers async.
        /// </summary>
        /// <param name="eventObject">The event.</param>
        /// <returns>Returns awaitable task.</returns>
        Task PublishAsync(Event eventObject);
    }
}
