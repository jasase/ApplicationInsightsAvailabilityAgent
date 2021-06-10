using System.Collections.Generic;

namespace ApplicationInsightsAvailabilityAgent.Core.Options
{
    public class CheckExecuterOptions
    {
#pragma warning disable CA2227 // It is configuration DTO
        public Dictionary<string, AvailabilityCheckOptions> Checks { get; set; }
#pragma warning restore CA2227 // Sammlungseigenschaften müssen schreibgeschützt sein
    }
}
