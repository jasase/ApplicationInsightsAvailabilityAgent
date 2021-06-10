using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public class HttpChecker : Checker
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CheckerOptions<HttpCheckerOptions> _options;

        public HttpChecker([NotNull] ILogger<HttpChecker> logger,
                           IHttpClientFactory httpClientFactory,
                           CheckerOptions<HttpCheckerOptions> options)
            : base(logger)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        public override async Task<bool> Execute(CancellationToken cancellationToken)
        {
            var t = 0;
            return false;
        }
    }
}
