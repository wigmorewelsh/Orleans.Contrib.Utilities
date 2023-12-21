using Microsoft.Extensions.DependencyInjection;

namespace Orleans.Utilities;

public static class ServiceCollectionLocatorExtensions
{
    public static IServiceCollection AddGrainLocators(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IIntergerGrainLocator<>), typeof(IntergerGrainLocator<>));
        services.AddSingleton(typeof(IStringGrainLocator<>), typeof(StringGrainLocator<>));
        services.AddSingleton(typeof(IGuidGrainLocator<>), typeof(GuidGrainLocator<>));
        services.AddSingleton(typeof(IGuidCompoundGrainLocator<>), typeof(GuidCompoundGrainLocator<>));
        services.AddSingleton(typeof(IIntegerCompoundGrainLocator<>), typeof(IntegerCompoundGrainLocator<>));
        return services;
    }
}