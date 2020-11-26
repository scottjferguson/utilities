using Core.Plugins;
using Core.Plugins.Configuration;
using Core.Plugins.FileHandling;
using FluentCommander.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pluralize.NET.Core;
using Processor;
using Utilities.Common.Providers;
using Utilities.Domain.Customer.Context;

namespace Utilities
{
    public class Startup : IStartup
    {
        private readonly PluginConfiguration _pluginConfiguration;

        public Startup(IConfiguration configuration)
        {
            _pluginConfiguration = BuildPluginConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework
            services.AddDbContext<CustomerDbContext>();

            // Add the DatabaseCommander framework
            services.AddSqlServerDatabaseCommander(_pluginConfiguration.Configuration);

            // Add Core Plugins
            services.AddApplicationServices(_pluginConfiguration);
            services.AddCorePlugins(_pluginConfiguration);
            services.AddFileHandlingPlugin();

            // Add other services needed to run the application
            services.AddTransient<Pluralizer>();
            services.AddTransient<RandomNumberProvider>();
            services.AddTransient<RandomCodeProvider>();
        }

        private PluginConfiguration BuildPluginConfiguration(IConfiguration configuration)
        {
            return new PluginConfigurationBuilder()
                .UseConfiguration(configuration)
                .UseApplicationName("Utilities")
                .UseServiceLifetime(ServiceLifetime.Transient)
                .UseGlobalUsername(configuration["GlobalUsername"])
                .Build();
        }
    }
}
