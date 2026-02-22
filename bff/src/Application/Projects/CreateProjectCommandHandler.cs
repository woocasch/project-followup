namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class CreateProjectCommandHandler(
    IEventStreamsRepository streamsRepository,
    ILogger<CreateProjectCommandHandler> logger)
    : CommandHandlerBase<CreateProjectCommand>(logger)
{
    protected override async Task<CommandResult> HandleCommand(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = ProjectAggregateRoot.Create(
            command.ProjectId,
            command.Title,
            command.Description,
            command.CreatedAt);
        await streamsRepository.StoreStreamAsync(project, cancellationToken);
        return CommandResult.Success();
    }
}
