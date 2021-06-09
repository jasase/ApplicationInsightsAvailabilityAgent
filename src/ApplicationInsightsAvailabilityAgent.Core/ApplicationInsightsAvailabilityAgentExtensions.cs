using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public static class ApplicationInsightsAvailabilityAgentExtensions
    {
        public static void AddAvailabilityChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<CheckExecuterOptions>().Bind(configuration);
            services.AddSingleton<ICheckerExecutorFactory, CheckerExecutorFactory>();
        }
    }
}
