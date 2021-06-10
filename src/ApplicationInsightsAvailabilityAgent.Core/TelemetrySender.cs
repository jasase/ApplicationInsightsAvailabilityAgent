using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public class TelemetrySender : ITelemetrySender
    {
        public void TrackAvailability(string instrumentationKey, AvailabilityTelemetry telemetry)
        {
            using var telemetryConfiguration = new TelemetryConfiguration
            {
                InstrumentationKey = instrumentationKey
            };
            var telemetryClient = new TelemetryClient(telemetryConfiguration);

            telemetryClient.TrackAvailability(telemetry);

            telemetryClient.Flush();
        }
    }
}
