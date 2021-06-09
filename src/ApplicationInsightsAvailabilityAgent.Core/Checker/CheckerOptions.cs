namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public abstract class CheckerOptions
    {
        public string Name { get; set; }
        public string InstrumentationKey { get; set; }
    }

    public class CheckerOptions<TOptions> : CheckerOptions
    {
        public TOptions Options { get; set; }
    }
}
