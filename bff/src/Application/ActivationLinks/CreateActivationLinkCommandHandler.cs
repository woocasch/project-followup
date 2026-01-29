namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventsBus;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.ActivationLink.DomainEvents;
using ProjectFollowUp.BFF.Domain.User;

public sealed class CreateActivationLinkCommandHandler(
    IEventStreamsRepository eventsRepository,
    IEventPublisher eventPublisher) : CommandHandlerBase<CreateActivationLinkCommand>
{
    protected override async Task<CommandResult> HandleCommand(CreateActivationLinkCommand command, CancellationToken cancellationToken)
    {
        var activationLink = ActivationLinkAggregateRoot.Create(
            command.UserId,
            command.LinkCode);
        await eventsRepository.StoreStreamAsync(
            activationLink,
            cancellationToken);
        var linkGeneratedEvent = new ActivationLinkGenerated
        {
            ActivationLinkId = activationLink.Id,
        };
        await eventPublisher.Publish(
            linkGeneratedEvent,
            cancellationToken);
        return CommandResult.Success();
    }
}
