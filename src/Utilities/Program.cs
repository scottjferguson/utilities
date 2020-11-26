using Processor;
using System.Threading.Tasks;

namespace Utilities
{
    class Program : ProcessorProgram<Startup>
    {
        static async Task Main()
        {
            await RunAsync();
        }
    }
}
