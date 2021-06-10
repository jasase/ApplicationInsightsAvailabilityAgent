using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpCheckerFactory : CheckerFactory<HttpCheckerOptions>
    {
        public HttpCheckerFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        public override string CheckerType => "http";

        protected override Checker CreateCheckerInternal(CheckerOptions<HttpCheckerOptions> options)
            => new HttpChecker(ServiceProvider.GetService<ILogger<HttpChecker>>(), ServiceProvider.GetService<IHttpClientFactory>(), options);
    }
}
