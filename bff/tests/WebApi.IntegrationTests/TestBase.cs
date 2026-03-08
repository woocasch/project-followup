namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Security.Claims;

using Bogus;

using Microsoft.AspNetCore.Mvc.Testing;

[Collection(IntegrationTestsFixture.CollectionName)]
public abstract class TestBase
{
    private static readonly Faker<TestUserBuilder.TestUser> testUserGenerator = new Faker<TestUserBuilder.TestUser>()
        .CustomInstantiator(f => new TestUserBuilder.TestUser(
            Guid.NewGuid(),
            f.Internet.UserName(),
            []));

    private readonly WebApiFactory webApiFactory;

    private readonly string[] roles;

    private readonly Lazy<HttpClient> client;

    protected TestBase(WebApiFactory webApiFactory, params string[] roles)
    {
        this.webApiFactory = webApiFactory;
        this.roles = roles;
        this.client = new Lazy<HttpClient>(this.CreateClient);
    }

    protected TestBase(WebApiFactory webApiFactory)
        : this(webApiFactory, [])
    {
    }

    protected HttpClient Client => this.client.Value;

    protected TestUserBuilder.TestUser GenerateUser()
    {
        var user = testUserGenerator.Generate();
        user = user with
        {
            Claims = user.Claims.Concat(this.roles.Select(role => (ClaimTypes.Role, role))).ToList().AsReadOnly(),
        };
        return user;
    }

    protected HttpClient CreateClient()
    {
        var clientInstance = this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
        clientInstance.Timeout = TimeSpan.FromSeconds(30);
        var user = this.GenerateUser();
        clientInstance.WithTestUser(user.UserId, user.UserName, [.. user.Claims]);
        return clientInstance;
    }
}
