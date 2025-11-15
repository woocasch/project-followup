namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class CreateProjectCommandHandler : CommandHandlerBase<CreateProjectCommand>
{
    protected override Task<CommandResult> HandleCommand(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(CommandResult.Success());
    }
}
