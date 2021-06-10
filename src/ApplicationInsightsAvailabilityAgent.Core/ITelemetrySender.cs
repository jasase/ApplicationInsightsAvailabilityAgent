using Microsoft.ApplicationInsights.DataContracts;

namespace ApplicationInsightsAvailabilityAgent.Core
{
    public interface ITelemetrySender
    {
        void TrackAvailability(string instrumentationKey, AvailabilityTelemetry telemetry);
    }
}
