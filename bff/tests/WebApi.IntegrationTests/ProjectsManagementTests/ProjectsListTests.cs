namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net.Http.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

public sealed class ProjectsListTests(WebApiFactory webApiFactory) : TestBase(webApiFactory)
{
    private Guid projectId;

    private HttpRequestMessage request = null!;

    private HttpResponseMessage response = null!;

    private FetchListOutput fetchProjectsOutput = null!;

    [Fact]
    public void WhenProjectIsCreatedThenItIsPresentInFetchedList()
    {
        var projectTitle = nameof(WhenProjectIsCreatedThenItIsPresentInFetchedList);
        this.Given(t => t.ProjectIsCreated(projectTitle))
            .And(t => t.FetchRequestIsCreated())
            .When(t => t.FetchRequestIsSent())
            .And(t => t.FetchResponseIsDeserialized())
            .Then(t => t.OutputContainsProjectWithId(this.projectId))
            .BDDfy();
    }

    private async Task ProjectIsCreated(string title)
    {
        var createProjectPayload = new CreateInput(title, "Description");
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

    private void FetchRequestIsCreated()
    {
        this.request = new HttpRequestMessage(HttpMethod.Get, "/api/Projects");
    }

    private async Task FetchRequestIsSent()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        this.response = await this.Client.SendAsync(this.request);
    }

    private async Task FetchResponseIsDeserialized()
    {
        this.response.EnsureSuccessStatusCode();
        var output = await this.response.Content.ReadFromJsonAsync<FetchListOutput>();
        output.ShouldNotBeNull();
        this.fetchProjectsOutput = output;
    }

    private void OutputContainsProjectWithId(Guid id)
    {
        this.fetchProjectsOutput.Projects.ShouldContain(p => p.Id == id);
    }
}
