using Core.Application;
using Core.Plugins.Extensions;
using Core.Plugins.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilities
{
    class Program
    {
        static async Task Main()
        {
            string processName = Console.ReadLine();

            if (string.IsNullOrEmpty(processName))
                throw new ArgumentException("Please provide a process name to run");

            processName = processName.Trim();

            List<Type> processorTypes = new AssemblyScanner()
                .GetApplicationTypesWithAttribute<ProcessorAttribute>(p => string.Equals(p.Name, processName, StringComparison.InvariantCultureIgnoreCase));

            if (!processorTypes.Any())
                throw new ArgumentException($"No Processor with Name of '{processName}' was found");

            if (processorTypes.Count > 1)
                throw new ArgumentException($"Multiple Processors with Name of '{processName}' were found");

            Type processorType = processorTypes.Single();

            if (!processorType.GetInterfaces().Contains(typeof(IProcessor)))
                throw new ArgumentException($"Processor with Name of '{processName}' does not implement IProcessor");

            IServiceProvider serviceProvider = Startup();

            IProcessor processor = (IProcessor)serviceProvider.GetService(processorType);

            try
            {
                await processor.ProcessAsync();

                Log("Complete");
            }
            catch (Exception e)
            {
                LogException($"{processName} failed", e);
            }

            Console.ReadLine();
        }

        private static IServiceProvider Startup()
        {
            // Build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(BasePath)
                .AddJsonFile("local.settings.json", false)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", true)
                .Build();

            // Generate the Service Provider
            IServiceProvider serviceProvider = new Startup(config).ConfigureServices();

            return serviceProvider;
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static void LogException(string message, Exception e)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Exception message: {e.Message}");
        }

        private static string BasePath => AppDomain.CurrentDomain.BaseDirectory.SubstringBefore("bin");
        private static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    }
}
