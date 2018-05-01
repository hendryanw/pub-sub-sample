using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Domain
{
    /// <summary>
    /// An interface to access configuration data repository.
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Update the configuration entity.
        /// </summary>
        /// <param name="configuration">The configuration entity.</param>
        void Update(Configuration configuration);

        /// <summary>
        /// Update the configuration entity async.
        /// </summary>
        /// <param name="configuration">The configuration entity.</param>
        /// <returns>Returns awaitable task.</returns>
        Task UpdateAsync(Configuration configuration);
    }
}
