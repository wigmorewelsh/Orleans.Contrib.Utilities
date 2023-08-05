using Microsoft.Extensions.DependencyInjection;

namespace Orleans.Utils;

public static class ServiceCollectionIdentifierExtensions
{
    public static IServiceCollection AddGrainIdentifier(this IServiceCollection services)
    {
        services.AddScoped<IGrainIdentifier, GrainIdentifier>();
        return services;
    }
}