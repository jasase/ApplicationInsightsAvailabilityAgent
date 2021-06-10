namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class CheckerResult
    {
        public CheckerResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        public bool Result { get; }
        public string Message { get; }
    }
}
