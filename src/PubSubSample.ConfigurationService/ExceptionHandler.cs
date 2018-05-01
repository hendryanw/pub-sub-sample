using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PubSubSample.ConfigurationService
{
    public class ExceptionHandler
    {
        private readonly ILoggerFactory _loggerFactory;

        public ExceptionHandler(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"An error occurred while processing your request. Request Id: {Activity.Current?.Id ?? context.TraceIdentifier}");

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            var logger = _loggerFactory.CreateLogger<ExceptionHandler>();

            if (ex != null)
            {
                logger.LogError(ex, $"Exception caught on the exception handler. Request Id: {Activity.Current?.Id ?? context.TraceIdentifier}");
            }
            else
            {
                logger.LogError($"Exception handler is invoked. No exception information found. Request Id: {Activity.Current?.Id ?? context.TraceIdentifier}");
            }
        }
    }
}
