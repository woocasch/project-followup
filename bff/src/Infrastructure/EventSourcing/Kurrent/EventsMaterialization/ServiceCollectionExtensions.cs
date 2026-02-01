namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaterializers(
        this IServiceCollection services)
    {
        services
            .AddScoped<IReadModelHydrator, ReadModelHydrator>();
        return services;
    }
}
