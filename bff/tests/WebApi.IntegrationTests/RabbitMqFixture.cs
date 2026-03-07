namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using DotNet.Testcontainers.Builders;

using ProjectFollowUp.BFF.RabbitMqSetup;

using Testcontainers.RabbitMq;

public sealed class RabbitMqFixture : IAsyncLifetime
{
    private static readonly Lazy<RabbitMqFixture> LazyInstance = new(() => new RabbitMqFixture());

    private readonly RabbitMqContainer container;

    private RabbitMqFixture()
    {
        this.container = new RabbitMqBuilder("rabbitmq:4.0-management")
            .WithCleanUp(true)
            .WithPortBinding(15672, true)
            .WithPortBinding(5672, true)
            .WithUsername("admin")
            .WithPassword("admin")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(5672))
            .Build();
    }

    public static RabbitMqFixture Instance => LazyInstance.Value;

    public string ConnectionString => $"amqp://api-bff:api-bff@{this.container.Hostname}:{this.container.GetMappedPublicPort(5672)}/projectfollowup";

    public async Task InitializeAsync()
    {
        await container.StartAsync();
        await this.SetupQueues();
    }

    public async Task DisposeAsync()
    {
        await container.StopAsync();
    }

    private async Task SetupQueues()
    {
        var setupSettings = new SetupSettings()
        {
            RabbitMqRestUrl = $"http://{this.container.Hostname}:{this.container.GetMappedPublicPort(15672)}",
            RabbitMqRestUsername = "admin",
            RabbitMqRestPassword = "admin"
        };

        var rabbitMqClient = SetupHelpers.CreateRabbitRestClient(setupSettings);

        await OperationsRunner.RunOperations(
            rabbitMqClient,
            CancellationToken.None);
    }
}
