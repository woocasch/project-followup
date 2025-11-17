namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Projects.ProjectEvents;

public sealed class UpdateProjectCommandHandler : CommandHandlerBase<UpdateProjectCommand>
{
    protected override async Task<CommandResult> HandleCommand(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var domainEvent = new ProjectDetailsChanged(
            command.ProjectId,
            command.Title,
            command.Description,
            command.UserId,
            command.CreatedAt);
        ProjectsStore.AddEvent(command.ProjectId, domainEvent);
        return CommandResult.Success();
    }
}
