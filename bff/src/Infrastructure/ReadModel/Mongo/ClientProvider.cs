namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

public sealed class ClientProvider(
    IOptions<MongoSettings> settingsOptions) : IClientProvider
{
    private readonly MongoSettings settings = settingsOptions.Value;

    public IMongoClient GetClient()
    {
        return new MongoClient(settings.ConnectionString);
    }
}
