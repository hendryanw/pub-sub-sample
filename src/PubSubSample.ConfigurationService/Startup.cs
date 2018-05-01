using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using PubSubSample.ConfigurationService.Models;
using PubSubSample.Domain;
using PubSubSample.Infrastructure.Redis;
using StackExchange.Redis;

namespace PubSubSample.ConfigurationService
{
    public class Startup
    {
        public readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventPublisher, EventPublisher>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();

            var redisConnectionString = Configuration.GetSection("ConnectionStrings")["Redis"];
            var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(redisConnectionMultiplexer);

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Configuration Service", Version = "v1" });
            });
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.CreateMap<ConfigurationUpdateModel, Configuration>(MemberList.Destination);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                loggerFactory.AddConsole();
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                loggerFactory.AddConsole();
                app.UseExceptionHandler(
                    new ExceptionHandlerOptions
                    {
                        ExceptionHandler = new ExceptionHandler(loggerFactory).Invoke
                    });
            }
            else
            {
                throw new NotSupportedException($"The environment {env.EnvironmentName} is not supported.");
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Configuration Service v1");
            });
        }
    }
}
