using Microsoft.Extensions.Configuration;

namespace ApplicationInsightsAvailabilityAgent.Core.Options
{
    public class AvailabilityCheckOptions
    {
        public string InstrumentationKey { get; set; }
        public string Method { get; set; }
        //public Dictionary<string, string> Options { get; set; }
        public IConfigurationSection Options { get; set; }


    }
}
