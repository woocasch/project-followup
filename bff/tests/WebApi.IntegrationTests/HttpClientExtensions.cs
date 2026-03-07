namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Net.Http.Headers;

public static class HttpClientExtensions
{
    public static HttpClient WithTestUser(
        this HttpClient client,
        string userId,
        string userName,
        params (string Type, string Value)[] claims)
    {
        var token = TestAuthTokenGenerator.GenerateToken(userId, userName, claims);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public static HttpClient WithTestUser(
        this HttpClient client,
        Guid userId,
        string userName,
        params (string Type, string Value)[] claims)
    {
        return WithTestUser(client, userId.ToString(), userName, claims);
    }
}
