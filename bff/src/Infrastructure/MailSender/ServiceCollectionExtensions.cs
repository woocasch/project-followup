namespace ProjectFollowUp.BFF.Infrastructure.MailSender;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailSender(
        this IServiceCollection services)
    {
        services.AddScoped<IMailSender, SmtpMailSender>();
        return services;
    }
}
