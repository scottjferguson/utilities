using Core.Application;
using Core.FileHandling;
using Core.Plugins.Microsoft.Azure.Wrappers;
using Core.Plugins.Utilities;
using Core.Plugins.Utilities.FileHandling.Delimited;
using Core.Plugins.Utilities.FileHandling.Excel;
using Core.Providers;
using FluentCommander.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Utilities.Common.Providers;
using Utilities.Domain.Customer.Context;

namespace Utilities
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices()
        {
            // Initialize a ServiceCollection and add logging
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug);
                    builder.AddConsole();
                });

            // Add the DatabaseCommander framework
            serviceCollection.AddSqlServerDatabaseCommander(_configuration);

            // Add Entity Framework
            serviceCollection.AddDbContext<CustomerDbContext>();

            // Add all Processors
            AddProcessors(serviceCollection);

            // Add other services needed to run the application
            serviceCollection.AddSingleton(_configuration);
            serviceCollection.AddTransient<RandomNumberProvider>();
            serviceCollection.AddTransient<RandomCodeProvider>();
            serviceCollection.AddTransient<IDelimitedFileHandler, GenericParsingDelimitedFileHandler>();
            serviceCollection.AddTransient<IExcelFileHandler, ClosedXmlExcelHandler>();
            serviceCollection.AddTransient<IConnectionStringProvider, AzureConnectionStringByConfigurationProvider>();

            // Build the IServiceProvider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private void AddProcessors(IServiceCollection serviceCollection)
        {
            List<Type> processorTypes = new AssemblyScanner()
                .GetApplicationTypesWithAttribute<ProcessorAttribute>();

            foreach (Type processorType in processorTypes)
            {
                serviceCollection.AddTransient(processorType);
            }
        }
    }
}
