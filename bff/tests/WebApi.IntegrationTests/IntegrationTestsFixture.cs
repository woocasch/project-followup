namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;


[CollectionDefinition(CollectionName)]
public sealed class IntegrationTestsFixture : ICollectionFixture<WebApiFactory>
{
    public const string CollectionName = "IntegrationTests";
}