namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.WebApi;


// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    private readonly KurrentDbFixture kurrentDbFixture = KurrentDbFixture.Instance;

    private readonly MongoDbFixture mongoDbFixture = MongoDbFixture.Instance;

    private readonly RabbitMqFixture rabbitMqFixture = RabbitMqFixture.Instance;

    private readonly MailPitFixture mailPitFixture = MailPitFixture.Instance;

    public async Task InitializeAsync()
    {
        await Task.WhenAll(
            this.kurrentDbFixture.InitializeAsync(),
            this.mongoDbFixture.InitializeAsync(),
            this.rabbitMqFixture.InitializeAsync(),
            this.mailPitFixture.InitializeAsync());
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.WhenAll(
            this.kurrentDbFixture.DisposeAsync(),
            this.mongoDbFixture.DisposeAsync(),
            this.rabbitMqFixture.DisposeAsync(),
            this.mailPitFixture.DisposeAsync());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration((_, bld) =>
        {
            var configuration = new Dictionary<string, string?>()
            {
                ["Kurrent:ConnectionString"] = this.kurrentDbFixture.ConnectionString,
                ["Mongo:ConnectionString"] = this.mongoDbFixture.ConnectionString,
                ["Mongo:DatabaseName"] = "ProjectFollowUpIntegrationTests",
                ["EventBus:ConnectionString"] = this.rabbitMqFixture.ConnectionString,
                ["MailSettings:host"] = this.mailPitFixture.Host,
                ["MailSettings:port"] = this.mailPitFixture.Port.ToString(),
            };
            bld.AddInMemoryCollection(configuration);
        });

        builder.ConfigureServices(services =>
        {
            // Remove the existing JWT Bearer authentication
            services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    _ => { });
        });

        builder.UseEnvironment("IntegrationTests");
    }
}