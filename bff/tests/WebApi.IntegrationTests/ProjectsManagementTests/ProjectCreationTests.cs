namespace ProjectFollowUp.BFF.WebApi.IntegrationTests.ProjectsManagementTests;

using System.Net;
using System.Net.Http.Json;

using Newtonsoft.Json;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Validation;

public sealed class ProjectCreationTests(WebApiFactory webApiFactory) : TestBase(webApiFactory)
{
    private CreateInput payload = null!;

    private HttpRequestMessage request = null!;

    private HttpResponseMessage response = null!;

    [Fact]
    public void WhenValidProjectIsCreatedThenCorrectStatusIsReturned()
    {
        this.Given(t => t.PayloadIsCreated(nameof(WhenValidProjectIsCreatedThenCorrectStatusIsReturned), "Description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.Created))
            .BDDfy();
    }

    [Fact]
    public void WhenValidProjectIsCreatedThenProjectIdIsReturnedInBody()
    {
        this.Given(t => t.PayloadIsCreated(nameof(WhenValidProjectIsCreatedThenCorrectStatusIsReturned), "Description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseBodyShouldBeNotNull<CreateOutput>())
            .Then(t => t.ResponseBodyShouldBe<CreateOutput>(o => o.ProjectId != Guid.Empty))
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
            .And(t => t.ResponseBodyShouldBeNotNull<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Title" &&
                    entry.Code == "MaxLength100")))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectTitleContainsLineBreakThenBadRequestIsReturned()
    {
        var titleWithLineBreak = "This is a title with a line break.\nHere is the second line.";
        this.Given(t => t.PayloadIsCreated(titleWithLineBreak, "Valid description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.BadRequest))
            .And(t => t.ResponseBodyShouldBeNotNull<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Title" &&
                    entry.Code == "ForbiddenCharacters")))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectTitleContainsWindowsLineBreakThenCreatedIsReturned()
    {
        var titleWithLineBreak = "This is a title with a line break.\r\nHere is the second line.";
        this.Given(t => t.PayloadIsCreated(titleWithLineBreak, "Valid description"))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.BadRequest))
            .And(t => t.ResponseBodyShouldBeNotNull<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Title" &&
                    entry.Code == "ForbiddenCharacters")))
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
            .And(t => t.ResponseBodyShouldBeNotNull<ValidationErrorsDetails>())
            .And(t => t.ResponseBodyShouldBe<ValidationErrorsDetails>(body =>
                body.Errors.Any(entry =>
                    entry.PropertyName == "Description" &&
                    entry.Code == "MaxLength500")))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectDescriptionContainsLineBreakThenCreatedIsReturned()
    {
        var descriptionWithLineBreak = "This is a description with a line break.\nHere is the second line.";
        this.Given(t => t.PayloadIsCreated("Valid Title", descriptionWithLineBreak))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.Created))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectDescriptionContainsWindowsLineBreakThenCreatedIsReturned()
    {
        var descriptionWithLineBreak = "This is a description with a line break.\r\nHere is the second line.";
        this.Given(t => t.PayloadIsCreated("Valid Title", descriptionWithLineBreak))
            .And(t => t.RequestIsCreated())
            .When(t => t.RequestIsSent())
            .Then(t => t.ResponseStatusShouldBe(HttpStatusCode.Created))
            .BDDfy();
    }

    private void PayloadIsCreated(string title, string description)
    {
        this.payload = new CreateInput(title, description);
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

    private async Task ResponseBodyShouldBeNotNull<T>()
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
