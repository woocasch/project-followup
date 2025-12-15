namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class CreateProjectCommandHandler : CommandHandlerBase<CreateProjectCommand>
{
    protected override Task<CommandResult> HandleCommand(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var domainEvent = new Domain.Project.Events.ProjectCreated(
            command.ProjectId,
            command.Title,
            command.Description,
            command.CreatedAt);
        UsersStore.AddEvent(domainEvent.ProjectId, domainEvent);
        return Task.FromResult(CommandResult.Success());
    }
}
