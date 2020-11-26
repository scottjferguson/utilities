using Processor;
using System;
using System.Threading;

namespace Utilities.Domain.Framework
{
    public class ProcessorOrchestrationBase : UtilitiesBase
    {
        protected async void Execute<TProcessor>(CancellationToken cancellationToken) where TProcessor : IProcessor, new()
        {
            Log($"Starting {typeof(TProcessor).Name}...");

            try
            {
                var processor = new TProcessor();

                await processor.ProcessAsync(cancellationToken);

                Log("Complete");
            }
            catch (Exception e)
            {
                LogException($"Failed with message: {e.Message}", e);
            }
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void LogException(string message, Exception e)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Exception message: {e.Message}");
        }
    }
}
