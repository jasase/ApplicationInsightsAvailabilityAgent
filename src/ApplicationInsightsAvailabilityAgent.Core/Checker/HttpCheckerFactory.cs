namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpCheckerFactory : CheckerFactory<HttpCheckerOptions>
    {
        public override string CheckerType => "http";

        protected override Checker CreateCheckerInternal(CheckerOptions<HttpCheckerOptions> options)
        {
            return new HttpChecker(null, null, options);
        }
    }
}
