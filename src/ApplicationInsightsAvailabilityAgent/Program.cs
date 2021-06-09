using ApplicationInsightsAvailabilityAgent.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationInsightsAvailabilityAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((c, s) =>
                {
                    s.AddAvailabilityChecks(c.Configuration);
                    s.AddHostedService<CheckService>();
                })
                .Build()
                .Run();
        }
    }
}
