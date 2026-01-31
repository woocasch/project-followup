namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

public sealed class DatabaseProvider(
    IClientProvider clientProvider,
    IOptions<MongoSettings> settingsOptions) : IDatabaseProvider
{
    private readonly MongoSettings settings = settingsOptions.Value;

    public IMongoDatabase GetDatabase()
    {
        var client = clientProvider.GetClient();
        return client.GetDatabase(settings.DatabaseName);
    }
}
