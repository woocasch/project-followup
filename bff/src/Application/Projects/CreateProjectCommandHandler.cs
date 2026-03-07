namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class CreateProjectCommandHandler(
    IEventStreamsRepository streamsRepository,
    ILogger<CreateProjectCommandHandler> logger)
    : CommandHandlerBase<CreateProjectCommand>(logger)
{
    protected override async Task<CommandResult> HandleCommand(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        logger.Started(command.ProjectId.Value);
        var project = ProjectAggregateRoot.Create(
            command.ProjectId,
            command.Title,
            command.Description,
            command.CreatedAt);
        logger.StoringNewStream(command.ProjectId.Value);
        await streamsRepository.StoreStreamAsync(project, cancellationToken);
        logger.Completed(command.ProjectId.Value);
        return CommandResult.Success();
    }
}
