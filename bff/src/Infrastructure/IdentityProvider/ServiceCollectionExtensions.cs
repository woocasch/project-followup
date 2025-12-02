namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.IdentityProvider;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpIdentityProvider(
        this IServiceCollection services)
    {
        services.AddTransient<IIdentityProviderClient, HttpIdentityProvider>();
        return services;
    }
}
