namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization.User;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaterializers(
        this IServiceCollection services)
    {
        services
            .AddUserMaterializers()
            .AddScoped<IReadModelHydrator, ReadModelHydrator>();
        return services;
    }

    internal static IServiceCollection AddEventMaterializer<TEvent, TMaterializer>(
        this IServiceCollection services)
        where TMaterializer : class, IEventMaterializer
    {
        var key = typeof(TEvent).AssemblyQualifiedName ?? throw new InvalidOperationException("Event type must have a full name.");
        services.AddKeyedTransient<IEventMaterializer, TMaterializer>(key);
        return services;
    }
}
