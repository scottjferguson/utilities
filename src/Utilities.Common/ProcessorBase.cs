using Core.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities.Common
{
    public abstract class ProcessorBase<TToProcess> : ProcessorBase
    {
        protected virtual void PreProcess(TToProcess objectToPreProcess)
        {

        }

        protected virtual void PostProcess(TToProcess objectToPostProcess)
        {

        }
    }

    public abstract class ProcessorBase : UtilitiesBase, IProcessor
    {
        public abstract Task ProcessAsync(CancellationToken cancellationToken);

        protected virtual void OnException(Exception e)
        {
            throw e;
        }
    }
}
