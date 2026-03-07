namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

public sealed class MailPitFixture : IAsyncLifetime
{
    private static readonly Lazy<MailPitFixture> LazyInstance = new(() => new MailPitFixture());

    private readonly IContainer container;

    private MailPitFixture()
    {
        this.container = new ContainerBuilder("axllent/mailpit:latest")
            .WithPortBinding(1025, true)
            .WithPortBinding(8025, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(1025))
            .WithCleanUp(true)
            .Build();
    }

    public static MailPitFixture Instance => LazyInstance.Value;

    public string Host => this.container.Hostname;

    public ushort Port => this.container.GetMappedPublicPort(1025);

    public string WebUiUrl => $"http://{this.container.Hostname}:{this.container.GetMappedPublicPort(8025)}";

    public async Task InitializeAsync()
    {
        await container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await container.StopAsync();
    }
}
