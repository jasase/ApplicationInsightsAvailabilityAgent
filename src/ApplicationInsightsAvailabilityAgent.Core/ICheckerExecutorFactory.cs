using ApplicationInsightsAvailabilityAgent.Core.Options;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public interface ICheckerExecutorFactory
    {
        CheckerExecutor CreateCheckExecuter(CheckExecuterOptions options);
    }
}
