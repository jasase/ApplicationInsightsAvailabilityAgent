using System.Threading;
using System.Threading.Tasks;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutor
    {
        private Checker.Checker[] checkers;

        public CheckerExecutor(Checker.Checker[] checker)
        {
            this.checkers = checker;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            foreach (var checker in checkers)
            {
                await checker.Execute(cancellationToken);

                //TODO Telemetry Client send result
            }
        }
    }
}
