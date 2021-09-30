using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class CheckerExecutor
    {
        private readonly ILogger<CheckerExecutor> _logger;
        private readonly ITelemetrySender _telemetrySender;
        private readonly Checker.Checker[] _checkers;
        private readonly CheckExecuterOptions _options;

        public CheckerExecutor(ILogger<CheckerExecutor> logger, ITelemetrySender telemetrySender, Checker.Checker[] checker, Options.CheckExecuterOptions options)
        {
            _logger = logger;
            _telemetrySender = telemetrySender;
            _checkers = checker;
            _options = options;
        }

        public string DumpCheckerConfiguration()
        {
            var sb = new StringBuilder();
            const string FORMAT = "{0,-40} | {1,-38} | {2,-10} | {3}";

            sb.AppendFormat(CultureInfo.InvariantCulture, FORMAT, "Name", "Instrumentation Key", "Method", "Options");
            sb.AppendLine();

            foreach (var checker in _checkers)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, FORMAT, checker.Name, checker.InstrumentationKey, checker.Method, checker.DumpOptions());
                sb.AppendLine();
            }

            return sb.ToString();
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
                    Message = message,
                    RunLocation = _options.RunLocation
                };
                _telemetrySender.TrackAvailability(checker.InstrumentationKey, telemetry);
            }
        }
    }
}
