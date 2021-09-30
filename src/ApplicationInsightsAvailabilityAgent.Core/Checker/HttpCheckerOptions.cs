using System;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpCheckerOptions
    {
        public Uri Uri { get; set; }

        public bool AddHttpBodyToResult { get; set; }
    }
}
