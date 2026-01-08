namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

using ProjectFollowUp.BFF.WebApi;


// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    private readonly KurrentDbFixture kurrentDbFixture = KurrentDbFixture.Instance;

    public async Task InitializeAsync()
    {
        await this.kurrentDbFixture.InitializeAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await this.kurrentDbFixture.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration((_, bld) =>
        {
            var configuration = new Dictionary<string, string?>()
            {
                ["Kurrent:ConnectionString"] = this.kurrentDbFixture.ConnectionString,
            };
            bld.AddInMemoryCollection(configuration);
        });

        builder.UseEnvironment("IntegrationTests");
    }
}