namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventsBus;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.ActivationLink.DomainEvents;
using ProjectFollowUp.BFF.Domain.User;

public sealed class CreateActivationLinkCommandHandler(
    IEventStreamsRepository eventsRepository,
    IEventPublisher eventPublisher,
    ILogger<CreateActivationLinkCommandHandler> logger) : CommandHandlerBase<CreateActivationLinkCommand>
{
    protected override async Task<CommandResult> HandleCommand(
        CreateActivationLinkCommand command,
        CancellationToken cancellationToken)
    {
        var activationLink = ActivationLinkAggregateRoot.Create(
            command.UserId,
            command.LinkCode);
        logger.AggregateCreated(
            activationLink.UserId.Value,
            activationLink.Id.Value);
        await eventsRepository.StoreStreamAsync(
            activationLink,
            cancellationToken);
        logger.StreamStored(
            activationLink.UserId.Value,
            activationLink.Id.Value);
        var linkGeneratedEvent = new ActivationLinkGenerated
        {
            ActivationLinkId = activationLink.Id,
        };
        await eventPublisher.Publish(
            linkGeneratedEvent,
            cancellationToken);
        logger.LinkGeneratedEventPublished(
            activationLink.UserId.Value,
            activationLink.Id.Value);
        logger.Completed(activationLink.UserId.Value);
        return CommandResult.Success();
    }
}
