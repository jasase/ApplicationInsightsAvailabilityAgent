using System;
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

        public override string Method => HttpCheckerFactory.CHECKER_TYPE_NAME;

        public HttpChecker([NotNull] ILogger<HttpChecker> logger,
                           IHttpClientFactory httpClientFactory,
                           CheckerOptions<HttpCheckerOptions> options)
            : base(logger, options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }


        public override async Task<CheckerResult> Execute(CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = _options.Options.Uri;

            var result = await httpClient.GetAsync("").ConfigureAwait(false);

            var message = $"Response status code: {result.StatusCode} ({(int) result.StatusCode})";
            if (_options.Options.AddHttpBodyToResult)
            {
                message += Environment.NewLine;
                message += await GetBody(result);
            }

            return new CheckerResult(result.IsSuccessStatusCode, message);
        }

        private async Task<string> GetBody(HttpResponseMessage responseMessage)
        {
            try
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public override string DumpOptions() => _options.Options.Uri.ToString();
    }
}
