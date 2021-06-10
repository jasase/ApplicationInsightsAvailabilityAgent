using System;
using ApplicationInsightsAvailabilityAgent.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationInsightsAvailabilityAgent.Core.Checker
{
    public abstract class CheckerFactory
    {
        public abstract string CheckerType { get; }

        public abstract Checker CreateChecker(string name, AvailabilityCheckOptions checkOptions);
    }

    public abstract class CheckerFactory<TOptions> : CheckerFactory
        where TOptions : class, new()
    {
        protected IServiceProvider ServiceProvider { get; }

        public CheckerFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public override Checker CreateChecker(string name, AvailabilityCheckOptions checkOptions)
        {
            var typedOptions = new TOptions();
            checkOptions.Options.Bind(typedOptions);

            return CreateCheckerInternal(new CheckerOptions<TOptions>
            {
                InstrumentationKey = checkOptions.InstrumentationKey,
                Name = name,
                Options = typedOptions
            });
        }

        protected abstract Checker CreateCheckerInternal(CheckerOptions<TOptions> options);
    }
}
