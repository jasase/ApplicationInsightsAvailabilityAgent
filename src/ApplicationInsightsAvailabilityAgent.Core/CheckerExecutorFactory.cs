using System;
using System.Linq;
using ApplicationInsightsAvailabilityAgent.Core.Checker;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutorFactory : ICheckerExecutorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly CheckerFactory[] _checkerFactories;

        public CheckerExecutorFactory(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;

            _checkerFactories = new[]
            {
                new HttpCheckerFactory(_serviceProvider)
            };
        }

        public CheckerExecutor CreateCheckExecuter(CheckExecuterOptions options)
        {
            var checker = options.Checks.SelectMany(x => _checkerFactories.Where(f => f.CheckerType.Equals(x.Value.Method, System.StringComparison.InvariantCultureIgnoreCase))
                                                                         .Select(f => f.CreateChecker(x.Key, x.Value)))
                                        .ToArray();



            return new CheckerExecutor(checker);
        }
    }
}
