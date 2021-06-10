using ApplicationInsightsAvailabilityAgent.Core;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationInsightsAvailabilityAgent
{
    public class CheckService : IHostedService
    {
        private readonly IOptionsMonitor<CheckExecuterOptions> _options;
        private readonly ICheckerExecutorFactory _checkFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public CheckService(IOptionsMonitor<CheckExecuterOptions> options, ICheckerExecutorFactory checkFactory)
        {
            _options = options;
            _checkFactory = checkFactory;
            _cancellationTokenSource = new CancellationTokenSource();
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
                var options = _options.CurrentValue;

                var executor = _checkFactory.CreateCheckExecuter(options);

                await executor.Execute(_cancellationTokenSource.Token).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }
    }
}
