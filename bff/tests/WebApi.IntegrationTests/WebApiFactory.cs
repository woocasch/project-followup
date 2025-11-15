namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using ProjectFollowUp.BFF.WebApi;


// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await Task.Yield();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.Yield();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment("IntegrationTests");
    }
}