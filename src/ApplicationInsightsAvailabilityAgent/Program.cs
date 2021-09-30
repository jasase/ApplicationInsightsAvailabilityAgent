using ApplicationInsightsAvailabilityAgent.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationInsightsAvailabilityAgent
{
    public class Program
    {
        public static void Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .UseWindowsService(options => options.ServiceName = "AppInsightAvailibilityAgent")
                   .ConfigureServices((c, s) =>
                   {
                       s.AddAvailabilityChecks(c.Configuration);
                       s.AddHostedService<CheckService>();
                   })
                   .Build()
                   .Run();
    }
}
