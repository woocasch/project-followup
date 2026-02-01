namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

public sealed class ClientProvider(
    IServiceProvider serviceProvider) : IClientProvider
{
    public IMongoClient GetClient()
    {
        return serviceProvider
            .GetRequiredService<IMongoClient>();
    }
}
