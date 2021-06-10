using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public abstract class Checker
    {
        private readonly CheckerOptions _checkerOptions;

        public string Name => _checkerOptions.Name;
        public string InstrumentationKey => _checkerOptions.InstrumentationKey;

        protected ILogger Logger { get; }

        public Checker(ILogger logger, CheckerOptions checkerOptions)
        {
            Logger = logger;
            _checkerOptions = checkerOptions;
        }

        public abstract Task<CheckerResult> Execute(CancellationToken cancellationToken);

    }
}
