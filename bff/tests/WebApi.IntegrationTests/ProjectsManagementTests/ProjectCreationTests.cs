namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net;
using System.Net.Http.Json;

using Newtonsoft.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Validation;

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

    [Fact]
    public void WhenProjectNameIsLongerThanAllowedThenBadRequestIsReturned()
    {
        var longTitle = new string('A', 101);
        this.Given(t => t.PayloadIsCreated(longTitle, "Description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.BadRequest))
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Title" &&
                    entry.Code == "MaxLength100")))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectDescriptionIsLongerThanAllowedThenBadRequestIsReturned()
    {
        var longDescription = new string('A', 501);
        this.Given(t => t.PayloadIsCreated("Valid Title", longDescription))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.BadRequest))
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Description" &&
                    entry.Code == "MaxLength500")))
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

    private async Task ResponseBodyShouldBe<T>()
        where T : class
    {
        var responseBody = await this.response.Content.ReadAsStringAsync();
        JsonConvert.DeserializeObject<T>(responseBody).ShouldNotBeNull();
    }

    private async Task ResponseBodyShouldBe<T>(Func<T, bool> predicate)
        where T : class
    {
        var responseBody = await this.response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<T>(responseBody);
        predicate(deserialized!).ShouldBeTrue();
    }
}
