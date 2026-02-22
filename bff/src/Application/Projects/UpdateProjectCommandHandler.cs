namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class UpdateProjectCommandHandler(
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory,
    ILogger<UpdateProjectCommandHandler> logger)
    : CommandHandlerBase<UpdateProjectCommand>(logger)
{
    protected override async Task<CommandResult> HandleCommand(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var existingProjectEvents = await eventsRepository.ReadStreamAsync<ProjectAggregateRoot>(
            command.ProjectId.ToGuid(),
            cancellationToken);
        var project = aggregateFactory.Create(existingProjectEvents, ProjectAggregateRoot.Rehydrate);
        project.ChangeDetails(command.Title, command.Description, command.ChangedAt);
        await eventsRepository.StoreStreamAsync(project, cancellationToken);
        return CommandResult.Success();
    }
}
