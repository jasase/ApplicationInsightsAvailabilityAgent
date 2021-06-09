using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpChecker : Checker
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly CheckerOptions<HttpCheckerOptions> options;

        public HttpChecker(ILogger<HttpChecker> logger,
                           IHttpClientFactory httpClientFactory,
                           CheckerOptions<HttpCheckerOptions> options)
            : base(logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options;
        }

        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            return false;
        }
    }
}
