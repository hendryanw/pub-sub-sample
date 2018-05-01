using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubSample.Domain
{
    /// <summary>
    /// Represents an event entity when the configuration is changed.
    /// The instances of this object is immutable.
    /// </summary>
    public class ConfigurationChangedEvent : Event
    {
        public const string EventName = "ConfigurationChanged";

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="key">The changed configuration key.</param>
        /// <param name="value">The new configuration value.</param>
        /// <param name="dateTime">The datetime which the configuration is updated.</param>
        public ConfigurationChangedEvent(string key, string value, DateTime dateTime)
            : base(EventName)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            Key = key;
            Value = value;
            DateTime = dateTime;
        }

        /// <summary>
        /// Get the configuration key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Get the new configuration value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Get the datetime which the configuration is updated.
        /// </summary>
        public DateTime DateTime { get; }
    }
}
