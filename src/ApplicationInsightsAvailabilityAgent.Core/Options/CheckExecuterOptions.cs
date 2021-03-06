using System;
using System.Collections.Generic;

namespace ApplicationInsightsAvailabilityAgent.Core.Options
{
    public class CheckExecuterOptions
    {
        public string RunLocation { get; set; }
        public TimeSpan CheckInterval { get; set; }

        public CheckExecuterOptions()
        {
            CheckInterval = TimeSpan.FromMinutes(5);
        }

#pragma warning disable CA2227 // It is configuration DTO
        public Dictionary<string, AvailabilityCheckOptions> Checks { get; set; }
#pragma warning restore CA2227 // Sammlungseigenschaften müssen schreibgeschützt sein
    }
}
