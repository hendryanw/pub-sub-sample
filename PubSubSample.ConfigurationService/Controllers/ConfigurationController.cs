using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PubSubSample.ConfigurationService.Models;
using PubSubSample.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PubSubSample.ConfigurationService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IEventPublisher _eventPublisher;

        public ConfigurationController(ILogger<ConfigurationController> logger,
            IMapper mapper,
            IConfigurationRepository configurationRepository,
            IEventPublisher eventPublisher)
        {
            _logger = logger;
            _mapper = mapper;
            _configurationRepository = configurationRepository;
            _eventPublisher = eventPublisher;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ConfigurationUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug($"{nameof(ModelState)} is not valid.", ModelState);
                return BadRequest(ModelState);
            }

            var configuration = _mapper.Map<Configuration>(model);
            await _configurationRepository.UpdateAsync(configuration);

            var configurationChangedEvent = new ConfigurationChangedEvent(model.Key, model.Value, DateTime.Now);
            await _eventPublisher.PublishAsync(configurationChangedEvent);

            return Ok();
        }
    }
}
