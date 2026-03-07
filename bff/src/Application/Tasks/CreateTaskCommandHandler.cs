namespace ProjectFollowUp.BFF.Application.Tasks;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class CreateTaskCommandHandler(
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory,
    ILogger<CreateTaskCommandHandler> logger)
    : CommandHandlerBase<CreateTaskCommand>(logger)
{
    protected override async Task<CommandResult> HandleCommand(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        logger.Started(command.ProjectId.Value, command.TaskId);
        var existingProjectEvents = (await eventsRepository.ReadStreamAsync<ProjectAggregateRoot>(
            command.ProjectId.ToGuid(),
            cancellationToken))
            .ToList();
        if (existingProjectEvents.Count == 0)
        {
            logger.ProjectNotFound(command.ProjectId.Value, command.TaskId); ;
            return CommandResult.Failure("ProjectNotFound");
        }

        var project = aggregateFactory.Create(existingProjectEvents, ProjectAggregateRoot.Rehydrate);
        logger.ProjectRehydrated(command.ProjectId.Value, command.TaskId);
        project.AddTask(
            command.TaskId,
            command.Title,
            command.Description,
            command.DueDate,
            DateTimeOffset.UtcNow);
        logger.TaskAdded(command.ProjectId.Value, command.TaskId);
        await eventsRepository.StoreStreamAsync(project, cancellationToken);
        logger.Completed(command.ProjectId.Value, command.TaskId);
        return CommandResult.Success();
    }
}
