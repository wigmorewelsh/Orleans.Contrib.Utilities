

using Microsoft.Extensions.DependencyInjection;
using Orleans.Runtime;
using Orleans.Timers;

namespace Orleans.Utilities;

public interface IGrainTimer {
    public IDisposable RegisterTimer(Func<object, Task> asyncCallback, object state, TimeSpan dueTime, TimeSpan period);
}

public class GrainTimer : IGrainTimer
{
    private readonly IGrainContext grainContext;
    private readonly ITimerRegistry timerRegistry;

    public GrainTimer(IGrainContext grainContext, ITimerRegistry timerRegistry){
        this.grainContext = grainContext;
        this.timerRegistry = timerRegistry;
    }

    public IDisposable RegisterTimer(Func<object, Task> asyncCallback, object state, TimeSpan dueTime, TimeSpan period)
    {
        return timerRegistry.RegisterTimer(grainContext, asyncCallback, state, dueTime, period);
    }
}


public static class ServiceCollectionIdentifierExtensions
{
    public static IServiceCollection AddGrainTimer(this IServiceCollection services)
    {
        services.AddScoped<IGrainTimer, GrainTimer>();
        return services;
    }
}