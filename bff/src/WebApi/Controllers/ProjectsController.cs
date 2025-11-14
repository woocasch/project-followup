namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IResult FetchList()
    {
        var projects = new List<ProjectListItem>
        {
            new(
                Guid.NewGuid(),
                "Project Alpha",
                "Description for Project Alpha",
                usersCount: 5,
                tasksCompleted: 10,
                tasksTotal: 20),
            new(
                Guid.NewGuid(),
                "Project Beta",
                "Description for Project Beta",
                usersCount: 3,
                tasksCompleted: 7,
                tasksTotal: 15),
        };
        var result = new FetchListResult(projects);
        return Results.Ok(result);
    }
}
