namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net;
using System.Net.Http.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public class ProjectCreationTests : TestBase
{
    private CreatePayload payload = null!;

    private HttpRequestMessage request = null!;

    private HttpResponseMessage response = null!;

    public ProjectCreationTests(WebApiFactory webApiFactory)
        : base(webApiFactory)
    {
    }

    [Fact]
    public void WhenValidProjectIsCreatedThenCorrectStatusIsReturned()
    {
        this.Given(t => t.PayloadIsCreated(nameof(WhenValidProjectIsCreatedThenCorrectStatusIsReturned), "Description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.Accepted))
            .BDDfy();
    }

    private void PayloadIsCreated(string title, string description)
    {
        this.payload = new CreatePayload(title, description);
    }

    private void RequestIsCreated()
    {
        this.request = new HttpRequestMessage(HttpMethod.Post, "/api/projects")
        {
            Content = JsonContent.Create(this.payload),
        };
    }

    private async Task RequestIsSent()
    {
        this.response = await this.Client.SendAsync(this.request);
    }

    private void ResponseStatusShouldBe(HttpStatusCode statusCode)
    {
        this.response.StatusCode.ShouldBe(statusCode);
    }
}
