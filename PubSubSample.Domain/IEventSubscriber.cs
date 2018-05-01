using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Domain
{
    /// <summary>
    /// An interface to subscribe to events.
    /// </summary>
    public interface IEventSubscriber
    {
        /// <summary>
        /// Subscribe to an event topic. Execute the handler for each events found.
        /// </summary>
        /// <typeparam name="T">The event type. <see cref="Event"/></typeparam>
        /// <param name="topic">The topic or the name of the event.</param>
        /// <param name="handler">The action which will be executed for each events found.</param>
        void Subscribe<T>(string topic, Action<T> handler) where T : Event;

        /// <summary>
        /// Subscribe to an event topic. Execute the handler for each events found.
        /// </summary>
        /// <typeparam name="T">The event type. <see cref="Event"/></typeparam>
        /// <param name="topic">The topic or the name of the event.</param>
        /// <param name="handler">The action which will be executed for each events found.</param>
        /// <returns>Returns awaitable task.</returns>
        Task SubscribeAsync<T>(string topic, Action<T> handler) where T : Event;
    }
}
