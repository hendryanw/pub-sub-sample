using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubSample.Domain
{
    /// <summary>
    /// The configuration entity.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The key of the configuration.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value of the configuration.
        /// </summary>
        public string Value { get; set; }
    }
}
