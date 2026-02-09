namespace ProjectFollowUp.BFF.Application.Tasks;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class CreateTaskCommandHandler(
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory) : CommandHandlerBase<CreateTaskCommand>
{
    protected override async Task<CommandResult> HandleCommand(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var existingProjectEvents = (await eventsRepository.ReadStreamAsync<ProjectAggregateRoot>(
            command.ProjectId.ToGuid(),
            cancellationToken))
            .ToList();
        if (existingProjectEvents.Count == 0)
        {
            return CommandResult.Failure("ProjectNotFound");
        }

        var project = aggregateFactory.Create(existingProjectEvents, ProjectAggregateRoot.Rehydrate);
        project.AddTask(
            command.TaskId,
            command.Title,
            command.Description,
            command.DueDate,
            DateTimeOffset.UtcNow);
        await eventsRepository.StoreStreamAsync(project, cancellationToken);
        return CommandResult.Success();
    }
}
