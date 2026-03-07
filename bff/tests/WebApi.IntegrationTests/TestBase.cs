namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;

[Collection(IntegrationTestsFixture.CollectionName)]
public abstract class TestBase
{
    private readonly WebApiFactory webApiFactory;


    private readonly Lazy<HttpClient> client;

    protected TestBase(WebApiFactory webApiFactory)
    {
        this.webApiFactory = webApiFactory;
        this.client = new Lazy<HttpClient>(this.CreateClient);
    }

    protected HttpClient Client => this.client.Value;

    private HttpClient CreateClient()
    {
        var clientInstance = this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
        clientInstance.Timeout = TimeSpan.FromSeconds(30);

        // Add default test user authentication
        var defaultUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        clientInstance.WithTestUser(defaultUserId, "test.user@example.com");

        return clientInstance;
    }
}
