using PubSubSample.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PubSubSample.DeliveryService
{
    public class ConfigurationChangedListener
    {
        private readonly IEventSubscriber _eventSubscriber;

        public ConfigurationChangedListener(IEventSubscriber eventSubscriber)
        {
            _eventSubscriber = eventSubscriber ?? throw new ArgumentNullException(nameof(eventSubscriber));
        }

        public void Listen(string serviceId)
        {
            _eventSubscriber.Subscribe<ConfigurationChangedEvent>(ConfigurationChangedEvent.EventName, (eventObj) =>
            {
                Console.WriteLine($"{serviceId}: Configuration for key [{eventObj.Key}] has been changed to [{eventObj.Value}] on [{eventObj.DateTime.ToString()}]");
            });
        }
    }
}
