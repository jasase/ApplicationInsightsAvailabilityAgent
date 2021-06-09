using ApplicationInsightsAvailabilityAgent.Core.Checker;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutorFactory : ICheckerExecutorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly CheckerFactory[] checkerFactories = new[]
        {
            new HttpCheckerFactory()
        };

        public CheckerExecutorFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public CheckerExecutor CreateCheckExecuter(CheckExecuterOptions options)
        {
            var checker = options.Checks.SelectMany(x => checkerFactories.Where(f => f.CheckerType.Equals(x.Value.Method, System.StringComparison.InvariantCultureIgnoreCase))
                                                                         .Select(f => f.CreateChecker(x.Key, x.Value)))
                                        .ToArray();



            return new CheckerExecutor(checker);
        }
    }
}
