namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Security.Claims;

using Bogus;

using Microsoft.AspNetCore.Mvc.Testing;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

[Collection(IntegrationTestsFixture.CollectionName)]
public abstract class TestBase
{
    private static readonly Faker<TestUserBuilder.TestUser> testUserGenerator = new Faker<TestUserBuilder.TestUser>()
        .CustomInstantiator(f => new TestUserBuilder.TestUser(
            Guid.NewGuid(),
            f.Internet.UserName(),
            f.Internet.Email(),
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
        this.StoreUserInReadModel(user);
        return clientInstance;
    }

    private void StoreUserInReadModel(TestUserBuilder.TestUser user)
    {
        var mongoClient = new MongoClient(this.webApiFactory.MongoDb.ConnectionString);
        var database = mongoClient.GetDatabase("ProjectFollowUpIntegrationTests");
        var collection = database.GetCollection<UserDto>(CollectionProvider.UsersCollectionName);
        var userDto = new UserDto()
        {
            Id = user.UserId,
            CredentialsId = user.UserId,
            Email = user.Email,
            DisplayName = user.UserName,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        collection.InsertOne(userDto);
    }
}
