using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubSample.Domain
{
    /// <summary>
    /// A base class for event which is supported by <see cref="IEventPublisher"/> and <see cref="IEventSubscriber"/>.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Initialize an event by specifying its name.
        /// </summary>
        /// <param name="name">The event name.</param>
        public Event(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Get the event name.
        /// </summary>
        public string Name { get; }
    }
}
