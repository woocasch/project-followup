namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using ProjectFollowUp.BFF.WebApi;


// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    private static bool started = false;

    public async Task InitializeAsync()
    {
        if (started)
        {
            throw new InvalidOperationException("It was already started");   
        }

        await Task.Yield();
        started = true;
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