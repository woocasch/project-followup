namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net.Http.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public sealed class GetProjectTests : TestBase
{
    private Guid projectId;

    private HttpRequestMessage request = null!;

    private HttpResponseMessage response = null!;

    private GetOutput getProjectOutput = null!;

    public GetProjectTests(WebApiFactory webApiFactory)
        : base(webApiFactory)
    {
    }

    [Fact]
    public void WhenProjectIsCreatedThenItCanBeFetchedById()
    {
        var projectTitle = nameof(WhenProjectIsCreatedThenItCanBeFetchedById);
        var projectDescription = "Description for " + projectTitle;
        this.Given(t => t.ProjectIsCreated(projectTitle, projectDescription))
            .And(t => t.GetProjectRequestIsCreated())
            .When(t => t.GetProjectRequestIsSent())
            .And(t => t.ProjectOutputIsDeserialized())
            .Then(t => t.OutputContainsCorrectData(projectTitle, projectDescription))
            .BDDfy();
    }

    private void GetProjectRequestIsCreated()
    {
        this.request = new HttpRequestMessage(HttpMethod.Get, $"/api/Projects/{this.projectId}");
    }

    private async Task GetProjectRequestIsSent()
    {
        await Task.Delay(3000);
        this.response = await this.Client.SendAsync(this.request);
    }

    private async Task ProjectOutputIsDeserialized()
    {
        this.response.EnsureSuccessStatusCode();
        var output = await this.response.Content.ReadFromJsonAsync<GetOutput>();
        output.ShouldNotBeNull();
        this.getProjectOutput = output;
    }

    private void OutputContainsCorrectData(string title, string description)
    {
        this.getProjectOutput.Id.ShouldBe(this.projectId);
        this.getProjectOutput.Title.ShouldBe(title);
        this.getProjectOutput.Description.ShouldBe(description);
    }

    private async Task ProjectIsCreated(string title, string description)
    {
        var createProjectPayload = new CreateInput(title, description);
        var createProjectRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Projects")
        {
            Content = JsonContent.Create(createProjectPayload),
        };
        var createProjectResponse = await this.Client.SendAsync(createProjectRequest);
        createProjectResponse.EnsureSuccessStatusCode();
        var createProjectOutput = await createProjectResponse.Content.ReadFromJsonAsync<CreateOutput>();
        createProjectOutput.ShouldNotBeNull();
        this.projectId = createProjectOutput.ProjectId;
    }
}
