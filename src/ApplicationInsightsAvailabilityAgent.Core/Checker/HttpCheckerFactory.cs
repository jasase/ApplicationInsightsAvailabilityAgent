using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpCheckerFactory : CheckerFactory<HttpCheckerOptions>
    {
        public const string CHECKER_TYPE_NAME = "http";

        public HttpCheckerFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        public override string CheckerType => CHECKER_TYPE_NAME;

        protected override Checker CreateCheckerInternal(CheckerOptions<HttpCheckerOptions> options)
            => new HttpChecker(ServiceProvider.GetService<ILogger<HttpChecker>>(), ServiceProvider.GetService<IHttpClientFactory>(), options);
    }
}
