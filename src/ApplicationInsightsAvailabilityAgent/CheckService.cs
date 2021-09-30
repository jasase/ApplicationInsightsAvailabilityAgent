using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationInsightsAvailabilityAgent.Core;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationInsightsAvailabilityAgent
{
    public class CheckService : IHostedService
    {
        private readonly ILogger<CheckService> _logger;
        private readonly IOptionsMonitor<CheckExecuterOptions> _options;
        private readonly ICheckerExecutorFactory _checkFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private int _runCounter;

        public CheckService(ILogger<CheckService> logger, IOptionsMonitor<CheckExecuterOptions> options, ICheckerExecutorFactory checkFactory)
        {
            _logger = logger;
            _options = options;
            _checkFactory = checkFactory;
            _cancellationTokenSource = new CancellationTokenSource();
            _runCounter = 0;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(() => Run(), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        private async Task Run()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                _logger.LogInformation("Execute check run number: {0}", _runCounter);

                var options = _options.CurrentValue;

                var executor = _checkFactory.CreateCheckExecuter(options);

                if (_runCounter % 10 == 0)
                {
                    _logger.LogInformation(executor.DumpCheckerConfiguration());
                }

                await executor.Execute(_cancellationTokenSource.Token).ConfigureAwait(false);

                _logger.LogInformation("Check run number {0} finished", _runCounter++);

                await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }
    }
}
