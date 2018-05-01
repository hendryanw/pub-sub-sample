using PubSubSample.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PubSubSample.Infrastructure.Redis
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public void Update(Configuration configuration)
        {
        }

        public Task UpdateAsync(Configuration configuration)
        {
            return Task.CompletedTask;
        }
    }
}
