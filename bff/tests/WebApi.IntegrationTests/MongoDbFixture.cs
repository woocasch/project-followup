namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using DotNet.Testcontainers.Builders;

using Testcontainers.MongoDb;

public sealed class MongoDbFixture : IAsyncLifetime
{
    private static readonly Lazy<MongoDbFixture> LazyInstance = new(() => new MongoDbFixture());

    private readonly MongoDbContainer container;

    private MongoDbFixture()
    {
        this.container = new MongoDbBuilder("mongo:8.0")
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(27017))
            .Build();
    }

    public static MongoDbFixture Instance => LazyInstance.Value;

    public string ConnectionString => this.container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await container.StopAsync();
    }
}
