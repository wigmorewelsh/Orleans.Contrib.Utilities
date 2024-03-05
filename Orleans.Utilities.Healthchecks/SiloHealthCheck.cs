using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans.Runtime;

namespace Orleans.Utilities.Healthchecks;

public class SiloHealthCheck : IHealthCheck
{
    private DateTime lastCheckTime;
    private readonly IEnumerable<IHealthCheckParticipant> participants;

    public SiloHealthCheck(IEnumerable<IHealthCheckParticipant> participants)
    {
        this.participants = participants;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var lastChecked = this.lastCheckTime;
        this.lastCheckTime = DateTime.UtcNow;
        foreach (var participant in this.participants)
        {
            if (!participant.CheckHealth(lastChecked, out var reason))
            {
                var result = new HealthCheckResult(context.Registration.FailureStatus, $"Silo health check participant '{participant.GetType().Name}' returned unhealthy: {reason}");
                return Task.FromResult(result);
            }
        }

        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
