namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using DotNet.Testcontainers.Builders;

using Testcontainers.KurrentDb;

public sealed class KurrentDbFixture : IAsyncLifetime
{
    private static readonly Lazy<KurrentDbFixture> LazyInstance = new(() => new KurrentDbFixture());

    private readonly KurrentDbContainer container;

    private KurrentDbFixture()
    {
        this.container = new KurrentDbBuilder("docker.kurrent.io/kurrent-latest/kurrentdb:latest")
        .WithCleanUp(true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(2113))
        .Build();
    }

    public static KurrentDbFixture Instance => LazyInstance.Value;

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
