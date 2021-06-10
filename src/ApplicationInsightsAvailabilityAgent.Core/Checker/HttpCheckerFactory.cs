using System;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpCheckerFactory : CheckerFactory<HttpCheckerOptions>
    {
        public HttpCheckerFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        public override string CheckerType => "http";

        protected override Checker CreateCheckerInternal(CheckerOptions<HttpCheckerOptions> options)
        {
            var t = 0;
            return new HttpChecker(null, null, options);
        }
    }
}
