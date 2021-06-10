using System;
using System.Linq;
using ApplicationInsightsAvailabilityAgent.Core.Checker;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutorFactory : ICheckerExecutorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CheckerFactory[] _checkerFactories;

        public CheckerExecutorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _checkerFactories = new[]
            {
                new HttpCheckerFactory(_serviceProvider)
            };
        }

        public CheckerExecutor CreateCheckExecuter(CheckExecuterOptions options)
        {
            var checker = options.Checks.SelectMany(x => _checkerFactories.Where(f => f.CheckerType.Equals(x.Value.Method, StringComparison.InvariantCultureIgnoreCase))
                                                                         .Select(f => f.CreateChecker(x.Key, x.Value)))
                                        .ToArray();

            return new CheckerExecutor(_serviceProvider.GetService<ILogger<CheckerExecutor>>(), _serviceProvider.GetService<ITelemetrySender>(), checker);
        }
    }
}
