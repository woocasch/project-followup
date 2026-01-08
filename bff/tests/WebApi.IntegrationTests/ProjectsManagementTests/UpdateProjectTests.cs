namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net.Http.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public sealed class UpdateProjectTests(
    WebApiFactory webApiFactory) : TestBase(webApiFactory)
{
    private Guid projectId;

    private HttpRequestMessage request = null!;

    private HttpResponseMessage response = null!;

    private GetOutput getProjectOutput = null!;

    [Fact]
    public void WhenProjectIsUpdatedThenNewDataIsReturnedFromGet()
    {
        var projectTitle = nameof(WhenProjectIsUpdatedThenNewDataIsReturnedFromGet);
        var projectDescription = "Description for " + projectTitle;
        this.Given(t => t.ProjectIsCreated(projectTitle, projectDescription))
            .And(t => t.ProjectIsUpdated("Updated " + projectTitle, "Updated description for " + projectTitle))
            .And(t => t.GetProjectRequestIsCreated())
            .When(t => t.GetProjectRequestIsSent())
            .And(t => t.ProjectOutputIsDeserialized())
            .Then(t => t.OutputContainsCorrectData("Updated " + projectTitle, "Updated description for " + projectTitle))
            .BDDfy();
    }

    private void GetProjectRequestIsCreated()
    {
        this.request = new HttpRequestMessage(HttpMethod.Get, $"/api/Projects/{this.projectId}");
    }

    private async Task GetProjectRequestIsSent()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
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

    private async Task ProjectIsUpdated(string title, string description)
    {
        var createProjectPayload = new UpdateInput(title, description);
        var createProjectRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Projects/{this.projectId}")
        {
            Content = JsonContent.Create(createProjectPayload),
        };
        var createProjectResponse = await this.Client.SendAsync(createProjectRequest);
        createProjectResponse.EnsureSuccessStatusCode();
    }
}
