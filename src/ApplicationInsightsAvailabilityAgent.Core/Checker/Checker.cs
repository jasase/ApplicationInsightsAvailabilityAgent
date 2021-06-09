using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public abstract class Checker
    {
        public Checker(ILogger logger)
        {
            Logger = logger;
        }

        public abstract Task<bool> Execute(CancellationToken cancellationToken);

        protected ILogger Logger { get; }
    }
}
