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
        private readonly IOptionsMonitor<CheckExecuterOptions> options;
        private readonly ICheckerExecutorFactory checkFactory;
        private readonly CancellationTokenSource cancellationTokenSource;

        public CheckService(IOptionsMonitor<CheckExecuterOptions> options, ICheckerExecutorFactory checkFactory)
        {
            this.options = options;
            this.checkFactory = checkFactory;
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(() => Run(), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        private async Task Run()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                var options = this.options.CurrentValue;

                var executor = this.checkFactory.CreateCheckExecuter(options);

                await executor.Execute(cancellationTokenSource.Token).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }
    }
}
