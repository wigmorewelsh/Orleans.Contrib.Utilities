using System.Diagnostics.Metrics;
using Microsoft.Extensions.Options;
using Orleans.Runtime;
using Prometheus;

namespace Orleans.Utilities.Prometheus;

public class GrainMetricCallFilter : IIncomingGrainCallFilter
{
    private Counter grainCounter = Metrics.CreateCounter("grain_calls", "", "grain_name", "grain_method");
    private Summary grainSummary = Metrics.CreateSummary("grain_call_summary", "", "grain_name", "grain_method");

    private readonly IOptions<GrainMetricsConfiguration> _configuration;

    public GrainMetricCallFilter(IOptions<GrainMetricsConfiguration> configuration)
    {
        _configuration = configuration;
    }

    public async Task Invoke(IIncomingGrainCallContext context)
    {
        if (context == null) return;

        var grainType = context.Grain.GetType();
        var implMethod = context.ImplementationMethod?.Name ?? context.InterfaceMethod?.Name ?? "no-name";

        IncrementGrainCounter(context, grainType, implMethod);
        using var _ = StartGrainSummary(context, grainType, implMethod);

        var fqMethodName = $"{grainType?.Name}.{implMethod}";

        var options = _configuration.Value;

        if (options.GrainDotMethodsBlackList.Contains(fqMethodName)
            || options.GrainBlackList.Contains(grainType?.Name)
            || options.MethodBlackList.Contains(implMethod))
        {
            await context.Invoke();
            return;
        }

        await context.Invoke();
    }

    public IDisposable? StartGrainSummary(IIncomingGrainCallContext context, Type grainType, string implMethod)
    {
        if (context.Grain is GrainReference || context.Grain is Grain)
        {
            try
            {
                return grainSummary.WithLabels(grainType.PrettyName(), implMethod).NewTimer();
            }
            catch
            {
            }
        }

        return null;
    }

    private void IncrementGrainCounter(IIncomingGrainCallContext context, Type grainType, string implMethod)
    {
        if (context.Grain is GrainReference || context.Grain is Grain)
        {
            try
            {
                grainCounter.WithLabels(grainType.PrettyName(), implMethod).Inc();
            }
            catch
            {
            }
        }
    }
}