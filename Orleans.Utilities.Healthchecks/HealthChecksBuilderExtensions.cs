using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Orleans.Utilities.Healthchecks;

public static class HealthChecksBuilderExtensions 
{
    public static IHealthChecksBuilder AddSiloCheck(this IHealthChecksBuilder builder, string name, 
        HealthStatus? failureStatus = null,
        IEnumerable<string>? tags = null,
        TimeSpan? timeout = null)
    {
        return builder.AddCheck<SiloHealthCheck>(name, failureStatus, tags, timeout);
    }
}