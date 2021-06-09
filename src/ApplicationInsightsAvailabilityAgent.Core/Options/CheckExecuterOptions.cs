using System.Collections.Generic;

namespace ApplicationInsightsAvailabilityAgent.Core.Options
{
    public class CheckExecuterOptions
    {
        public Dictionary<string, AvailabilityCheckOptions> Checks { get; set; }
    }
}
