using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutor
    {
        private readonly ILogger<CheckerExecutor> _logger;
        private readonly ITelemetrySender _telemetrySender;
        private readonly Checker.Checker[] _checkers;

        public CheckerExecutor(ILogger<CheckerExecutor> logger, ITelemetrySender telemetrySender, Checker.Checker[] checker)
        {
            _logger = logger;
            _telemetrySender = telemetrySender;
            _checkers = checker;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            foreach (var checker in _checkers)
            {
                _logger.LogDebug("Execute checker {name}", checker.Name);

                string message;
                var result = false;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    var checkerResult = await checker.Execute(cancellationToken).ConfigureAwait(false);
                    result = checkerResult.Result;
                    message = checkerResult.Message;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Checker {name} failed with exception", checker.Name);
                    message = ex.ToString();
                }
                stopwatch.Stop();

                _logger.LogInformation("Executed checker {name} in {duration}", checker.Name, stopwatch.Elapsed);
                var telemetry = new AvailabilityTelemetry()
                {
                    Duration = stopwatch.Elapsed,
                    Success = result,
                    Name = checker.Name,
                    Message = message
                };
                _telemetrySender.TrackAvailability(checker.InstrumentationKey, telemetry);
            }
        }
    }
}
