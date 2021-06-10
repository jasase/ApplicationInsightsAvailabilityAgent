using System.Threading;
using System.Threading.Tasks;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutor
    {
        private readonly Checker.Checker[] _checkers;

        public CheckerExecutor(Checker.Checker[] checker)
        {
            _checkers = checker;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            foreach (var checker in _checkers)
            {
                await checker.Execute(cancellationToken);

                //TODO Telemetry Client send result
            }
        }
    }
}
